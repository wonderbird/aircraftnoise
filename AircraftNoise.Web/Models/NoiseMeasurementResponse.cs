namespace AircraftNoise.Web.Models;

public class NoiseMeasurementResponse
{
    public double NoiseMeasurementDba { get; set; }
    public DateTime? TimestampUtc { get; set; }
    public bool HasMeasurement { get; set; }
}
