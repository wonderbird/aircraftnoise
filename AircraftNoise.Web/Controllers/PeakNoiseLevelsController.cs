using AircraftNoise.Core.Adapters.Outbound;
using AircraftNoise.Core.Domain;
using AircraftNoise.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AircraftNoise.Web.Controllers;

// TODO: Because this returns only a single peak measurement, it should be renamed to PeakNoiseLevelController
[ApiController]
[Route("[controller]")]
public class PeakNoiseLevelsController : ControllerBase
{
    private readonly ICanProvideMeasurements _measurementProvider;

    public PeakNoiseLevelsController(ICanProvideMeasurements measurementProvider)
    {
        _measurementProvider = measurementProvider;
    }

    [HttpPost]
    public async Task<ActionResult<NoiseMeasurementResponse>> Search(
        [FromBody] NoiseMeasurementRequest request
    )
    {
        // TODO: Clarify error handling - what happens if there is no data?
        var endTimeUtc = request.EndTimeUtc ?? DateTime.UtcNow;
        var duration = TimeSpan.FromMinutes(request.DurationMinutes);

        var range = (await _measurementProvider.GetMeasurementsBeforeAsync(endTimeUtc, duration));
        var peak = range.GetPeak();

        return new NoiseMeasurementResponse
        {
            NoiseMeasurementDba = peak.NoiseMeasurementDba,
            TimestampUtc = peak.TimestampUtc,
            HasMeasurement = true,
        };
    }
}
