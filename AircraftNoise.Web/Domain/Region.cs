namespace AircraftNoise.Web.Domain;

public readonly record struct Region(int Id, string Name)
{
    public string MessstationUrl => $"https://www.dfld.de/Mess/Mess.php?R={Id}";
}