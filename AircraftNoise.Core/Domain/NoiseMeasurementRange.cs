using System.Collections.ObjectModel;

namespace AircraftNoise.Core.Domain;

public class NoiseMeasurementRange
{
    private readonly List<NoiseMeasurement> _measurements;
    public ReadOnlyCollection<NoiseMeasurement> Measurements => _measurements.AsReadOnly();
    public bool IsEmpty => _measurements.Count == 0;

    public NoiseMeasurementRange(IEnumerable<NoiseMeasurement> measurements)
    {
        _measurements = measurements.ToList();
    }

    /// <summary>
    /// Identify peak in non empty measurements
    /// </summary>
    /// <returns>Null if no data or Measurement with highest <see cref="NoiseMeasurement.NoiseMeasurementDba">NoiseMeasurementDba</see> value</returns>
    public NoiseMeasurement? GetPeak()
    {
        if (IsEmpty)
            return null;

        return _measurements.OrderByDescending(m => m.NoiseMeasurementDba).First();
    }
}
