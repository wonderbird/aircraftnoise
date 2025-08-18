using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public interface ICanProvideMeasurements
{
    /// <summary>
    /// Read measurements for the past time period ending at the specified UTC time.
    /// </summary>
    /// <param name="endTimeUtc">Last timestamp to consider</param>
    /// <param name="duration">Interval length. If zero, then return only an exact match of endTimeUtc</param>
    /// <returns>List of measurements starting at or after endTimeUtc - duration and ending at or before endTimeUtc</returns>
    Task<IEnumerable<NoiseMeasurement>> GetNoiseMeasurementsForPastTimePeriodAsync(
        DateTime endTimeUtc,
        TimeSpan duration
    );
}
