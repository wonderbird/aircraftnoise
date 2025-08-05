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
            Timestamp = DateTime.Now,
            HasMeasurement = true
        };

        var endTime = request.EndTime ?? DateTime.Now;
        var endTimeUtc = request.EndTimeUtc ?? endTime;
        var duration = TimeSpan.FromMinutes(request.DurationMinutes);

        var measurements = (
            await _measurementProvider.GetNoiseMeasurementsForPastTimePeriodAsync(endTime, duration)
        ).ToList();
        if (measurements.Count > 0)
        {
            var peakMeasurement = measurements
                .OrderByDescending(m => m.NoiseMeasurementDba)
                .First();
            response = new NoiseMeasurementResponse
            {
                NoiseMeasurementDba = peakMeasurement.NoiseMeasurementDba,
                Timestamp = peakMeasurement.Timestamp,
                HasMeasurement = true
            };
        }

        return response;
    }
}
