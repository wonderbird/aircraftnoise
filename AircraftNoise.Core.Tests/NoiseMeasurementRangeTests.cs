using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Tests;

public class NoiseMeasurementRangeTests
{
    [Fact]
    public void GetPeak_EmptyMeasurements_ReturnsNull()
    {
        var subject = new NoiseMeasurementRange([]);
        var actual = subject.GetPeak();
        Assert.Null(actual);
    }

    [Fact]
    public void GetPeak_SomeMeasurements_ReturnsPeak()
    {
        var peakMeasurement = new NoiseMeasurement(
            new DateTime(2025, 9, 22, 7, 0, 0, DateTimeKind.Utc),
            50
        );
        var subject = new NoiseMeasurementRange(
            [
                peakMeasurement,
                new NoiseMeasurement(new DateTime(2025, 9, 22, 7, 1, 0, DateTimeKind.Utc), 0),
            ]
        );

        var actual = subject.GetPeak();
        Assert.Equal(peakMeasurement, actual!.Value);
    }
}
