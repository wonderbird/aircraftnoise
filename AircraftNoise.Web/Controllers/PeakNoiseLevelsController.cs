using AircraftNoise.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AircraftNoise.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class PeakNoiseLevelsController : ControllerBase
{
    [HttpGet]
    public ActionResult<NoiseMeasurementResponse> Get()
    {
        return new NoiseMeasurementResponse
        {
            NoiseMeasurementDba = 42.0
        };
    }
}