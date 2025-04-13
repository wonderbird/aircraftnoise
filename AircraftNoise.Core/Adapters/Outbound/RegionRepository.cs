using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public class RegionRepository : ICanFindRegion
{
    public Region FindRegion(Location _)
    {
        return new Region(3, "Köln/Bonn");
    }
}