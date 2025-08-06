using AircraftNoise.Core.Adapters.Outbound;
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
        // TODO: Replace temporary test data here with properly initialized empty response
        var response = new NoiseMeasurementResponse
        {
            NoiseMeasurementDba = 13.0,
            TimestampUtc = DateTime.UtcNow,
            HasMeasurement = true
        };

        var endTimeUtc = request.EndTimeUtc ?? DateTime.UtcNow;
        var duration = TimeSpan.FromMinutes(request.DurationMinutes);

        var measurements = (
            await _measurementProvider.GetNoiseMeasurementsForPastTimePeriodAsync(endTimeUtc, duration)
        ).ToList();
        if (measurements.Count > 0)
        {
            var peakMeasurement = measurements
                .OrderByDescending(m => m.NoiseMeasurementDba)
                .First();
            response = new NoiseMeasurementResponse
            {
                NoiseMeasurementDba = peakMeasurement.NoiseMeasurementDba,
                TimestampUtc = peakMeasurement.Timestamp,
                HasMeasurement = true
            };
        }

        return response;
    }
}
