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


// -------- Endpoints! ----------

//Get all tasks with optional filtering - Query parameters: isCompleted, priority, dueBefore
app.MapGet("/api/tasks", ([FromQuery] string? filter, string? sort, DateTime? date, bool? completed, Prio? priority) =>
{   
    if(filter != null && filter != "" && filter != "completed" && filter != "Completed" && filter != "dueBefore" && filter != "duebefore" && filter != "priority" && filter != "Priority") {
        return Results.BadRequest(new { success = false, data = "Invalid sort method [Completed, dueBefore, priority]", message = "Operation failed" });
    }
    if(sort != null && sort != "" && sort != "priority" && sort != "Priority" && sort != "created" && sort != "Created" && sort != "dueDate" && sort != "duedate") {
        return Results.BadRequest(new { success = false, data = "Invalid sort method [Priority, Completed, dueDate]", message = "Operation failed" });
    }
    if(service.listItems(filter, sort, date, completed, priority) == null) {
        return Results.BadRequest(new { success = false, data = "No tasks to list", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.listItems(filter, sort, date, completed, priority), message = "Operation completed successfully"});
});

//Get specific task by ID
app.MapGet("/api/tasks/{id}", (int id) => 
{
    if(service.findTask(id) == null) {
        return Results.BadRequest(new { success = false, data = "ID not found", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.findTask(id), message = "Operation completed successfully"});
});

//Create new task
app.MapPost("/api/tasks", (Tasc task) => 
{
    if(task == null) {
        return Results.BadRequest(new { success = false, data = "Invalid task arguments", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.addToList(task), message = "Operation completed successfully"});
});

//Update existing task
app.MapPut("/api/tasks/{id}", (int id, [FromBody] Tasc task) => 
{
    if(service.findTask(id) == null) {
        return Results.BadRequest(new { success = false, data = "ID not found", message = "Operation failed" });
    }

    var updated = service.updateTasc(id, title: task.title, description: task.description, isCompleted: task.isCompleted, priority: task.priority, dueDate: task.dueDate?.ToString());

    return Results.Ok(new { success = true, data = updated, message = "Operation completed successfully"});
}); 

//Delete task
app.MapDelete("/api/tasks/{id}", (int id) => 
{ 
    if(service.findTask(id) == null) {
        return Results.BadRequest(new { success = false, data = "ID not found", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.deleteTask(id), message = "Operation completed successfully"});
});

app.Run();

public partial class Program {};