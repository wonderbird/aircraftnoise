using System.Collections.ObjectModel;

namespace AircraftNoise.Core.Domain;

public class NoiseMeasurementRange
{
    private readonly List<NoiseMeasurement> _measurements;
    public ReadOnlyCollection<NoiseMeasurement> Measurements => _measurements.AsReadOnly();

    public NoiseMeasurementRange(IEnumerable<NoiseMeasurement> measurements)
    {
        _measurements = measurements.ToList();
    }

    public NoiseMeasurement GetPeak()
    {
        // TODO: Edge case "no data" is not considered
        return _measurements.OrderByDescending(m => m.NoiseMeasurementDba).First();
    }
}
