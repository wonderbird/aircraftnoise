using System.Net;
using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Adapters.Outbound;

public interface ICanFindLocation
{
    Location FindLocation(IPAddress? ipAddress);
}