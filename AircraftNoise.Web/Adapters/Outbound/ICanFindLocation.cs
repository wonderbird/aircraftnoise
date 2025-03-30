using System.Net;
using AircraftNoise.Web.Domain;

namespace AircraftNoise.Web.Adapters.Outbound;

public interface ICanFindLocation
{
    Location FindLocation(IPAddress? ipAddress);
}