using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public interface ICanProvideMeasurements
{
    Task<IEnumerable<Domain.NoiseMeasurement>> GetNoiseMeasurementsForPastTimePeriodAsync(DateTime end, TimeSpan duration);
}