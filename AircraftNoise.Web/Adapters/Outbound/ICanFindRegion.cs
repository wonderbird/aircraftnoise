using AircraftNoise.Web.Domain;

namespace AircraftNoise.Web.Adapters.Outbound;

public interface ICanFindRegion
{
    Region FindRegion(Location location);
}