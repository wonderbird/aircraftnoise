using AircraftNoise.Web.Domain;

namespace AircraftNoise.Web.Adapters.Outbound;

public class RegionLookupService
{
    public Region GetRegion(Location _)
    {
        return new Region(3, "Köln/Bonn");
    }
}