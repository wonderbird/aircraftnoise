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

    [HttpGet]
    public async Task<ActionResult<NoiseMeasurementResponse>> Get()
    {
        var endTime = DateTime.Now;
        var duration = TimeSpan.FromMinutes(5);
        
        var measurements = (await _measurementProvider.GetNoiseMeasurementsForPastTimePeriodAsync(endTime, duration)).ToList();
        if (!measurements.Any())
        {
            return new NoiseMeasurementResponse
            {
                NoiseMeasurementDba = 0.0,
                Timestamp = null,
                HasMeasurement = false
            };
        }
        
        var peakMeasurement = measurements.OrderByDescending(m => m.NoiseMeasurementDba).First();
        
        return new NoiseMeasurementResponse
        {
            NoiseMeasurementDba = peakMeasurement.NoiseMeasurementDba,
            Timestamp = peakMeasurement.Timestamp,
            HasMeasurement = true
        };
    }
}
