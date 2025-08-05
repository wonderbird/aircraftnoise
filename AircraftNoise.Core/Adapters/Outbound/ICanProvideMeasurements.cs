using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public interface ICanProvideMeasurements
{
    Task<IEnumerable<NoiseMeasurement>> GetNoiseMeasurementsForPastTimePeriodAsync(DateTime endTimeUtc, TimeSpan duration);
}