namespace AircraftNoise.Web.Models;

public class NoiseMeasurementRequest
{
    public DateTime? EndTime { get; set; }
    public int DurationMinutes { get; set; } = 5;
}
