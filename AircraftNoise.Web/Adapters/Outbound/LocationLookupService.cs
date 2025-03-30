using System.Net;
using AircraftNoise.Web.Domain;

namespace AircraftNoise.Web.Adapters.Outbound;

public class LocationLookupService : ICanFindLocation
{
    public Location FindLocation(IPAddress? _)
    {
        return new Location(50.8956, 7.1818);
    }
}