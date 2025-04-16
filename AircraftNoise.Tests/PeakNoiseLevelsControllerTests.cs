using System.Net.Http.Json;
using System.Text.Json;
using AircraftNoise.Web.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AircraftNoise.Tests;

public class PeakNoiseLevelsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    private readonly NoiseMeasurementRequest _searchRequest = new()
    {
        EndTime = TimeZoneInfo.ConvertTime(
            DateTime.Parse("2025-04-09T01:00:00"),
            TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin"),
            TimeZoneInfo.Utc),
        DurationMinutes = 5
    };


    public PeakNoiseLevelsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Search_ReturnsSuccessAndCorrectContentType()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/PeakNoiseLevels", _searchRequest);

        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task Search_ReturnsExpectedNoiseMeasurementValue()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/PeakNoiseLevels", _searchRequest);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<NoiseMeasurementResponse>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(result);
        Assert.False(result.HasMeasurement);
    }
}