using System.Net.Http.Json;
using System.Text.Json;
using AircraftNoise.Web.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AircraftNoise.Web.Tests;

public class PeakNoiseLevelsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const double ExpectedNoiseLevelInDataFile = 59.4;

    private readonly WebApplicationFactory<Program> _factory;

    private readonly NoiseMeasurementRequest _searchRequest = new()
    {
        EndTimeUtc = TimeZoneInfo.ConvertTimeToUtc(
            DateTime.Parse("2025-04-09T01:00:00"),
            TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin")
        ),
        DurationMinutes = 5,
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
        Assert.Equal(
            "application/json; charset=utf-8",
            response.Content.Headers.ContentType?.ToString()
        );
    }

    [Fact]
    public async Task Search_ReturnsExpectedNoiseMeasurementValue()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/PeakNoiseLevels", _searchRequest);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<NoiseMeasurementResponse>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.NotNull(result);
        Assert.Equal(ExpectedNoiseLevelInDataFile, result.NoiseMeasurementDba);
        Assert.True(result.HasMeasurement);
    }
}
