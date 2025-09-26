using System.Net;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Project1.Test;

public class PassResponse
{
    public bool? success { set; get; }
    public string? message { set; get; }
}
public class ErrResponse
{
    public bool? success { set; get; }
    public string? data { set; get; }
    public string? message { set; get; }
}

public class ProjectTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProjectTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task getAllPlanetsTest()
    {
        var response = await _client.GetAsync("/planets");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Planets recovered successfully");
    }


    [Fact]
    public async Task getAllMoonsTest()
    {
        var response = await _client.GetAsync("/moons");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Moons recovered successfully");
    }


    [Fact]
    public async Task getAllStarsTest()
    {
        var response = await _client.GetAsync("/stars");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Stars recovered successfully");
    }


    [Fact]
    public async Task getPlanetById()
    {
        var response = await _client.GetAsync("/planets/1011");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Planet recovered successfully");
    }


    [Fact]
    public async Task getMoonById()
    {
        var response = await _client.GetAsync("/moons/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Moon recovered successfully");
    }


    [Fact]
    public async Task getStarById()
    {
        var response = await _client.GetAsync("/stars/7");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Star recovered successfully");
    }


    [Fact]
    public async Task getPlanetsMoonsById()
    {
        var response = await _client.GetAsync("/planets/moons/1011");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Moons recovered successfully");
    }

    [Fact]
    public async Task getPlanetsStarsById()
    {
        var response = await _client.GetAsync("/planets/stars/1011");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Stars recovered successfully");
    }


    [Fact]
    public async Task getMoonsPlanetById()
    {
        var response = await _client.GetAsync("/moons/planet/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Planet recovered successfully");
    }


    [Fact]
    public async Task getStarsPlanetsById()
    {
        var response = await _client.GetAsync("/stars/planets/7");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PassResponse>();

        content?.message.Should().Be("Planets recovered successfully");
    }
    
    
    // [Fact]
    // public async Task updatePlanet()
    // {
    //     var planet = new { Name = "Earth", Description = "A planet in the Suns solar system"};

    //     var response = await _client.PutAsJsonAsync("/planets/1003", planet);
        
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    //     var content = await response.Content.ReadFromJsonAsync<PassResponse>();
    //     content?.message.Should().Be("Planet updated successfully");
    // }

}
