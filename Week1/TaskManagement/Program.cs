using Serilog;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc; // for [Annontations]
using taskManagement.models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); //read config from app settings
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

Console.WriteLine("Task Manager Started");  
Tasc newTask = new Tasc();



app.UseHttpsRedirection();

app.Run();