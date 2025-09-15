using Serilog;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc; // for [Annontations]
using taskManagement.models;
using taskManagement.service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); //read config from app settings
builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();  
}

app.UseHttpsRedirection();

TaskService service = new TaskService();

app.MapGet("/api/tasks", () =>
{
    if(service.listItems() == null)
    {
        Results.BadRequest(new { success = false, data = "No tasks to list", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.listItems(), message = "Operation completed successfully"});
});

app.MapGet("/api/tasks/{id}", (int id) => 
{ 
    return Results.Ok(new { success = true, data = service.findTask(id), message = "Operation completed successfully"});
});

app.MapPost("/api/tasks", () => 
{ 
    return Results.Ok(new { success = true, data = service.listItems(), message = "Operation completed successfully"});
});

app.MapPut("/api/tasks/{id}", (int id) => 
{ 
    return Results.Ok(new { success = true, data = service.listItems(), message = "Operation completed successfully"});
});

app.MapDelete("/api/tasks/{id}", (int id) => 
{ 
    return Results.Ok(new { success = true, data = service.deleteTask(id), message = "Operation completed successfully"});
});



app.Run();