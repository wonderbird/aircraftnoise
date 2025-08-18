using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public interface ICanFindRegion
{
    Region FindRegion(Location location);
}
