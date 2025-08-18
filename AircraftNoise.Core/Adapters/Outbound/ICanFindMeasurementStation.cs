using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public interface ICanFindMeasurementStation
{
    MeasurementStation FindMeasurementStation(Location location);
}
