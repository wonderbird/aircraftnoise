using System.Net;
using AircraftNoise.Core.Adapters.Outbound;
using AircraftNoise.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AircraftNoise.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class PeakNoiseLevelController : ControllerBase
{
    private readonly ICanProvideMeasurements _measurementProvider;
    private readonly ILogger<PeakNoiseLevelController> _logger;

    public PeakNoiseLevelController(
        ICanProvideMeasurements measurementProvider,
        ILogger<PeakNoiseLevelController> logger
    )
    {
        _measurementProvider = measurementProvider;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<NoiseMeasurementResponse>?> Search(
        [FromBody] NoiseMeasurementRequest request
    )
    {
        var endTimeUtc = request.EndTimeUtc ?? DateTime.UtcNow;
        var duration = TimeSpan.FromMinutes(request.DurationMinutes);

        _logger.LogDebug("Searching peak before {EndTime} for {Duration}", endTimeUtc, duration);
        var range = await _measurementProvider.GetMeasurementsBeforeAsync(endTimeUtc, duration);

        if (range.IsEmpty)
        {
            return null; // by convention, null is converted to HTTP/NO CONTENT (204).
        }

        var peak = range.GetPeak();

        return new NoiseMeasurementResponse
        {
            NoiseMeasurementDba = peak.NoiseMeasurementDba,
            TimestampUtc = peak.TimestampUtc,
            HasMeasurement = true,
        };
    }
}
