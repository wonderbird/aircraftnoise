namespace AircraftNoise.Web.Models;

public class NoiseMeasurementResponse
{
    public double NoiseMeasurementDba { get; set; }
    public DateTime? TimestampUtc { get; set; }

    // TODO: replace the HasMeasurement field with an appropriate HTTP response saying "no data, but that is ok".
    public bool HasMeasurement { get; set; }
}
