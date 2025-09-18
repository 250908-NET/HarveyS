﻿using System.Net;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Web.Test;

public class Response
{
    public bool? success { set; get; }
    //public string? error { set; get; }
    public string? message { set; get; }
    //public Tasc? task { set; get; }
}

public class ApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // ----------- Testing all endpoints ------------
    

    [Fact]
    public async Task createTask()
    {
        var task = new { title = "Make unit tests", description = "Write this code"};

        var response = await _client.PostAsJsonAsync("/api/tasks", task);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<Response>();
        content.success.Should().Be(true);
        content.message.Should().Be("Operation completed successfully");
    }

    [Fact]
    public async Task getAll()
    {
        var response = await _client.GetAsync("/api/tasks");
        
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<Response>();
        
        content.success.Should().Be(true);
        content.message.Should().Be("Operation completed successfully");
    }
    /*
    {
    "id": 0,
    "title": "string",
    "description": "string",
    "isCompleted": true,
    "priority": 0,
    "dueDate": "2025-09-18T14:49:56.315Z",
    "updatedAt": "2025-09-18T14:49:56.316Z"
    }
    */
}