﻿using System.Net;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Web.Test;

public class PassResponse
{
    public bool? success { set; get; }
    //public Tasc? task {set; get; }
    public string? message { set; get; }
}
public class ErrResponse
{
    public bool? success { set; get; }
    public string? data { set; get; }
    public string? message { set; get; }
}

public class ApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // ----------- Testing all endpoints ------------
    

    //Create a task
    [Fact]
    public async Task createTask()
    {
        var task = new { title = "Make unit tests", description = "Write this code"};

        var response = await _client.PostAsJsonAsync("/api/tasks", task);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<PassResponse>();
        content?.success.Should().Be(true);
        content?.message.Should().Be("Operation completed successfully");
    }

    //get all tasks
    [Fact]
    public async Task getAll()
    {
        var response = await _client.GetAsync("/api/tasks");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();
        
        content?.success.Should().Be(true);
        content?.message.Should().Be("Operation completed successfully");
    }

    //create a task and update it
    [Fact]
    public async Task updateTask()
    {
        var task0 = new { title = "Make unit tests", description = "Write this code"};
        var task = new { title = "Update this test", description = "This is a description"};

        var response0 = await _client.PostAsJsonAsync("/api/tasks", task0);
        var response = await _client.PutAsJsonAsync("/api/tasks/1", task);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<PassResponse>();
        content?.success.Should().Be(true);
        content?.message.Should().Be("Operation completed successfully");
    }
    
    //create 2 tasks and find it
    [Fact]
    public async Task findTask()
    {
        var task1 = new { title = "Make unit tests", description = "Write this code"};
        var task2 = new { title = "Make unit tests again", description = "Write this code again"};

        var response1 = await _client.PostAsJsonAsync("/api/tasks", task1);
        var response2 = await _client.PostAsJsonAsync("/api/tasks", task2);
        var response = await _client.GetAsync("/api/tasks/2");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<PassResponse>();
        content?.success.Should().Be(true);
        content?.message.Should().Be("Operation completed successfully");
    }

    //try to find a task that doesnt exist
    [Fact]
    public async Task ErrfindTask()
    {
        var response = await _client.GetAsync("/api/tasks/10");
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<ErrResponse>();
        content?.success.Should().Be(false);
        content?.data.Should().Be("ID not found");
        content?.message.Should().Be("Operation failed");
    }

    //create a task and delete it
    [Fact]
    public async Task deleteTask()
    {
        var task = new { title = "Make unit tests", description = "Write this code"};
        var destroyMe = await _client.PostAsJsonAsync("/api/tasks", task);

        var response = await _client.DeleteAsync("/api/tasks/1");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<PassResponse>();
        content?.success.Should().Be(true);
        content?.message.Should().Be("Operation completed successfully");
    }

    //try to delete a task that doesnt exist
    [Fact]
    public async Task ErrDeleteTask()
    {
        var response = await _client.DeleteAsync("/api/tasks/1");
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<PassResponse>();
        content?.success.Should().Be(false);
        content?.message.Should().Be("Operation failed");
    }
}