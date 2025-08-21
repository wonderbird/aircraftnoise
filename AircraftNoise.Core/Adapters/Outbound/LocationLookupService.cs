using System.Net;
using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public class LocationLookupService : ICanFindLocation
{
    public Location FindLocation(IPAddress? _)
    {
        return new Location(50.8956, 7.1818);
    }
}
