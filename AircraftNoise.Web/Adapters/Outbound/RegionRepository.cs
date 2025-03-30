using AircraftNoise.Web.Domain;

namespace AircraftNoise.Web.Adapters.Outbound;

public class RegionRepository : ICanFindRegion
{
    public Region FindRegion(Location _)
    {
        return new Region(3, "Köln/Bonn");
    }
}