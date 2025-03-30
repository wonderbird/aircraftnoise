using AircraftNoise.Web.Domain;

namespace AircraftNoise.Web.Adapters.Outbound;

public interface ICanFindMeasurementStation
{
    MeasurementStation FindMeasurementStation(Location location);
}