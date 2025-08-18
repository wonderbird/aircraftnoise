namespace AircraftNoise.Core.Domain;

public readonly record struct MeasurementStation(int Id, int RegionId, string Name)
{
    public string NoiseGraphUrl => $"https://www.dfld.de/Mess/Messwerte.php?R={RegionId}&S={Id}";
}
