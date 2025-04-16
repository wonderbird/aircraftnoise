using AircraftNoise.Core.Adapters.Outbound;
using AircraftNoise.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AircraftNoise.Web.Controllers;

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
    public async Task<ActionResult<NoiseMeasurementResponse>> Search([FromBody] NoiseMeasurementRequest request)
    {
        var response = new NoiseMeasurementResponse
        {
            NoiseMeasurementDba = 0.0,
            Timestamp = null,
            HasMeasurement = false
        };

        var endTime = request.EndTime ?? DateTime.Now;
        var duration = TimeSpan.FromMinutes(request.DurationMinutes);
        
        var measurements = (await _measurementProvider.GetNoiseMeasurementsForPastTimePeriodAsync(endTime, duration)).ToList();
        if (measurements.Count > 0)
        {
            var peakMeasurement = measurements.OrderByDescending(m => m.NoiseMeasurementDba).First();
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
