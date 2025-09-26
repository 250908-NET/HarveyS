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

// -------------------------- Planets -------------------------- //

app.MapGet("/planets", async (IPlanetService service) => 
{
    List<Planet> planets = await service.GetAllAsync();
    return Results.Ok(new { message = "Planets recovered successfully", data = planets});
});

app.MapGet("/planets/{id}", async (IPlanetService service, int id) =>
{
    var planet = await service.GetByIdAsync(id);
    return planet is not null ? Results.Ok(new { message = "Planet recovered successfully", data = planet }) : Results.NotFound();
});

app.MapGet("/planets/moons/{id}", async (IPlanetService service, int id) =>
{
    var moons = await service.GetMoonsByIdAsync(id);
    return moons is not null ? Results.Ok(new { message = "Moons recovered successfully", data = moons }) : Results.NotFound();
});

app.MapGet("/planets/stars/{id}", async (IPlanetService service, int id) =>
{
    var stars = await service.GetStarsByIdAsync(id);
    return stars is not null ? Results.Ok(new { message = "Stars recovered successfully", data = stars }) : Results.NotFound();
});

app.MapPost("/planets", async (ILogger<Program> logger, IPlanetService service, Planet planet) =>
{
    var createdPlanet = await service.CreateAsync(planet);
    return Results.Created($"/planets/{createdPlanet.PlanetId}", createdPlanet);
});

app.MapPut("/planets/{id}", async (ILogger<Program> logger, IPlanetService service, int id, Planet planet) =>
{
    if (! await service.Exists(id)) 
    {
        return Results.BadRequest();
    }

    await service.UpdateAsync(id, planet);
    var updatedPlanet = await service.GetByIdAsync(id);
    return Results.Ok(new { message = "Planet updated successfully", data = updatedPlanet } );
});

app.MapDelete("/planets/{id}", async (IPlanetService service, int id) =>
{
    await service.DeleteAsync(id);
    return Results.NoContent();
});

// -------------------------- Moons -------------------------- //

app.MapGet("/moons", async (ILogger<Program> logger, IMoonService service) =>
{
    List<Moon> moons = await service.GetAllAsync();
    return Results.Ok(new { message = "Moons recovered successfully", data = moons});
});

app.MapGet("/moons/planet/{id}", async (ILogger<Program> logger, IMoonService service, int id) => 
{
    var planet = await service.GetPlanetByIdAsync(id);
    return planet is not null ? Results.Ok(new { message = "Planet recovered successfully", data = planet }) : Results.NotFound();
});

app.MapGet("/moons/{id}", async (ILogger<Program> logger, IMoonService service, int id) =>
{
    var moon = await service.GetByIdAsync(id);
    return moon is not null ? Results.Ok(new { message = "Moon recovered successfully", data = moon }) : Results.NotFound();
});

app.MapPost("/moons", async (ILogger<Program> logger, IMoonService service, Moon moon) =>
{
    var createdMoon = await service.CreateAsync(moon);
    return Results.Created($"/moons/{createdMoon.MoonId}", createdMoon);
});

app.MapPut("/moons/{id}", async (ILogger<Program> logger, IMoonService service, int id, Moon moon) =>
{
    if (! await service.Exists(id)) 
    {
        return Results.BadRequest();
    }

    await service.UpdateAsync(id, moon);
    return Results.Ok(await service.GetByIdAsync(id));
});

app.MapDelete("/moons/{id}", async (IMoonService service, int id) =>
{
    await service.DeleteAsync(id);
    return Results.NoContent();
});


// -------------------------- Stars -------------------------- //

app.MapGet("/stars", async (IStarService service) =>
{
    List<Star> stars = await service.GetAllAsync();
    return Results.Ok(new { message = "Stars recovered successfully", data = stars});
});

app.MapGet("/stars/{id}", async (IStarService service, int id) =>
{
    var star = await service.GetByIdAsync(id);
    return star is not null ? Results.Ok(new { message = "Star recovered successfully", data = star }) : Results.NotFound();
});

app.MapGet("/stars/planets/{id}", async (IStarService service, int id) =>
{
    var planets = await service.GetPlanetsByIdAsync(id);
    return planets is not null ? Results.Ok(new { message = "Planets recovered successfully", data = planets }) : Results.NotFound();
});

app.MapPost("/stars", async (ILogger<Program> logger, IStarService service, Star star) =>
{
    var createdStar = await service.CreateAsync(star);
    return Results.Created($"/stars/{createdStar.StarId}", createdStar);
});

app.MapPut("/stars/{id}", async (ILogger<Program> logger, IStarService service, int id, Star star) =>
{
    if (! await service.Exists(id)) 
    {
        return Results.BadRequest();
    }

    await service.UpdateAsync(id, star);
    return Results.Ok(await service.GetByIdAsync(id));
});

app.MapDelete("/stars/{id}", async (IStarService service, int id) =>
{
    await service.DeleteAsync(id);
    return Results.NoContent();
});


app.Run();


public partial class Program {};