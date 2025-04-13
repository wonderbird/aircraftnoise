namespace AircraftNoise.Core.Domain;

public readonly record struct Region(int Id, string Name)
{
    public string MeasurementStationSelectionUrl => $"https://www.dfld.de/Mess/Mess.php?R={Id}";
}