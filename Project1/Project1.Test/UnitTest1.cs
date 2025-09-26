using System.Net;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Project1.Test;


public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Test1()
    {
        var response = await _client.GetAsync("/");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
