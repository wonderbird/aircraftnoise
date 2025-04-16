using System.Text.Json;
using AircraftNoise.Web.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AircraftNoise.Tests;

public class PeakNoiseLevelsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PeakNoiseLevelsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_ReturnsSuccessAndCorrectContentType()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/PeakNoiseLevels");

        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task Get_ReturnsExpectedNoiseMeasurementValue()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/PeakNoiseLevels");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<NoiseMeasurementResponse>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(result);
        Assert.False(result.HasMeasurement);
    }
}