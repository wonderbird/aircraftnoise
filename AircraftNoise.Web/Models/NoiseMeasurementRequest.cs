namespace AircraftNoise.Web.Models;

public class NoiseMeasurementRequest
{
    public DateTime? EndTimeUtc { get; set; }
    public int DurationMinutes { get; set; } = 5;
}
