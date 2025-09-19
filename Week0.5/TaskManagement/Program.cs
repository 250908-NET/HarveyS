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


/// <summary>
/// Get all tasks with optional filtering
/// </summary>
/// <param name="filter">String used to identify filter type. (ie. only select "completed" tasks)</param>
/// <param name="sort">String used to identify sort type. (ie. sort list by priority)</param>
/// <param name="date">DateTime variable to be used alongside dueBefore, createdAt, and dueDate sort/filter options</param>
/// <param name="completed">Bool object to be used alongside "completed" filter</param>
/// <param name="priority">Priority enum to be used alongside priority filter</param>
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

/// <summary>
/// Get a task by ID
/// </summary>
/// <param name="id">Identification number</param>
/// <returns>Returns Task response </returns>
app.MapGet("/api/tasks/{id}", (int id) => 
{
    if(service.findTask(id) == null) {
        return Results.BadRequest(new { success = false, data = "ID not found", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.findTask(id), message = "Operation completed successfully"});
});

/// <summary>
/// Post a task
/// </summary>
/// <param name="id">Identification number</param>
/// <param name="task">A task object with all required params and valid params (title, description [optional], isCompleted, priority, dueDate [optional]</param>
/// <returns>Returns posted Task </returns>
app.MapPost("/api/tasks", (Tasc task) => 
{
    if(task == null) {
        return Results.BadRequest(new { success = false, data = "Invalid task arguments", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.addToList(task), message = "Operation completed successfully"});
});

/// <summary>
/// Update a task
/// </summary>
/// <param name="id">Identification number to find the task for updating</param>
/// <param name="task">A task object with all required params and valid params (title, description [optional], isCompleted, priority, dueDate [optional]</param>
/// <returns>Returns updated Task </returns>
app.MapPut("/api/tasks/{id}", (int id, [FromBody] Tasc task) => 
{
    if(service.findTask(id) == null) {
        return Results.BadRequest(new { success = false, data = "ID not found", message = "Operation failed" });
    }

    var updated = service.updateTasc(id, title: task.title, description: task.description, isCompleted: task.isCompleted, priority: task.priority, dueDate: task.dueDate?.ToString());

    return Results.Ok(new { success = true, data = updated, message = "Operation completed successfully"});
}); 

/// <summary>
/// Delete a task
/// </summary>
/// <param name="id">Identification number to find the task for updating</param>
/// <returns>Returns the now deleted Task </returns>
app.MapDelete("/api/tasks/{id}", (int id) => 
{ 
    if(service.findTask(id) == null) {
        return Results.BadRequest(new { success = false, data = "ID not found", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.deleteTask(id), message = "Operation completed successfully"});
});

/// <summary>
/// Get statistics
/// </summary>
/// <returns>Returns amount of total tasks, completed tasks, overdue tasks, and each priority of task </returns>
app.MapGet("/api/tasks/statistics", () =>
{
    if(service.listItems(null, null, null, null, null) == null) {
        return Results.BadRequest(new { success = false, data = "No stats to list", message = "Operation failed" });
    }
    return Results.Ok(new { success = true, data = service.stats(), message = "Operation completed successfully"});
});

app.Run();

public partial class Program {};