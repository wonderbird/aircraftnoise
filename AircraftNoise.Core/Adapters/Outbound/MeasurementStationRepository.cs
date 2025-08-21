using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public class MeasurementStationRepository : ICanFindMeasurementStation
{
    private readonly ICanFindRegion _regionFinder;

    public MeasurementStationRepository(ICanFindRegion regionFinder)
    {
        _regionFinder = regionFinder;
    }

    public MeasurementStation FindMeasurementStation(Location location)
    {
        var region = _regionFinder.FindRegion(location);
        return new MeasurementStation(32, region.Id, "Rösrath-Forsbach");
    }
}
