using Microsoft.EntityFrameworkCore;
using Space.Data;
using Space.Models;
using Space.Services;
using Space.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

string CS = File.ReadAllText("../connection_string.env");

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SpaceDbContext>(options => options.UseSqlServer(CS));


builder.Services.AddScoped<IMoonRepository, MoonRepository>();
builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<IStarRepository, StarRepository>();

builder.Services.AddScoped<IMoonService, MoonService>();
builder.Services.AddScoped<IPlanetService, PlanetService>();
builder.Services.AddScoped<IStarService, StarService>();


Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/planets", async (IPlanetService service) => 
{
    Results.Ok(await service.GetAllAsync());
});

app.MapGet("/planets/{id}", async (IPlanetService service, int id) =>
{
    var planet = await service.GetByIdAsync(id);
    return planet is not null ? Results.Ok(planet) : Results.NotFound();
});

app.Run();