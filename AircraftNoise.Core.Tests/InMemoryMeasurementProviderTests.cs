using System.Globalization;
using AircraftNoise.Core.Adapters.Outbound;
using AircraftNoise.Core.Domain;

namespace AircraftNoise.Core.Tests;

public class InMemoryMeasurementProviderTests
{
    // TODO: Test edge cases later
    [Fact]
    public async Task GetNoiseMeasurementsForPastTimePeriodAsync_RequestedEndTimeMatchesFirstMeasurement_ReturnsFirstMeasurement()
    {
        var expectedNoiseLevel = 55.0;
        var expectedTimestampUtc = new DateTime(2024, 12, 31, 11, 0, 0, 0, 0, DateTimeKind.Utc);

        var dfldHtml = new DfldHtml(
            [new MeasurementValue("31.12.2024", "12:00:00", expectedNoiseLevel)]
        ).Render();

        var provider = new InMemoryMeasurementProvider(dfldHtml);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(
            expectedTimestampUtc,
            TimeSpan.Zero
        );

        List<NoiseMeasurement> expectedMeasurements =
        [
            new NoiseMeasurement(expectedTimestampUtc, expectedNoiseLevel),
        ];
        Assert.Equal(expectedMeasurements, measurements.ToList());
    }

    [Fact]
    public async Task GetNoiseMeasurementsForPastTimePeriodAsync_RequestedEndTimeMatchesLastMeasurement_ReturnsLastMeasurement()
    {
        var expectedNoiseLevel = 55.0;
        var expectedTimestampUtc = new DateTime(2024, 12, 31, 11, 0, 0, 0, 0, DateTimeKind.Utc);

        var dfldHtml = new DfldHtml(
            [
                new MeasurementValue("31.12.2024", "11:00:00", 60.0),
                new MeasurementValue("31.12.2024", "12:00:00", expectedNoiseLevel),
            ]
        ).Render();

        var provider = new InMemoryMeasurementProvider(dfldHtml);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(
            expectedTimestampUtc,
            TimeSpan.Zero
        );

        List<NoiseMeasurement> expectedMeasurements =
        [
            new NoiseMeasurement(expectedTimestampUtc, expectedNoiseLevel),
        ];
        Assert.Equal(expectedMeasurements, measurements.ToList());
    }

    [Fact]
    public async Task GetNoiseMeasurementsForPastTimePeriodAsync_RequestedEndTimeMatchesRange_ReturnsMeasurementList()
    {
        var expectedNoiseLevels = new List<double> { 42.0, 43.0, 44.0 };
        var expectedTimestampsUtc = new List<DateTime>
        {
            new DateTime(2024, 12, 31, 10, 58, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 12, 31, 10, 59, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 12, 31, 11, 0, 0, 0, 0, DateTimeKind.Utc),
        };

        var at_00_00 = new DateTime(2024, 12, 31, 0, 0, 0, 0, 0, DateTimeKind.Utc);
        var at_10_58 = at_00_00.AddHours(10).AddMinutes(58);
        var at_10_59 = at_00_00.AddHours(10).AddMinutes(59);
        var at_11_00 = at_00_00.AddHours(11).AddMinutes(0);
        List<NoiseMeasurement> expectedMeasurements =
        [
            new NoiseMeasurement(at_10_58, 42.0),
            new NoiseMeasurement(at_10_59, 43.0),
            new NoiseMeasurement(at_11_00, 44.0),
        ];

        var dfldHtml = new DfldHtml(
            [
                new MeasurementValue("31.12.2024", "11:00:00", 60.0),
                new MeasurementValue("31.12.2024", "11:58:00", 42.0),
                new MeasurementValue("31.12.2024", "11:59:00", 43.0),
                new MeasurementValue("31.12.2024", "12:00:00", 44.0),
            ]
        ).Render();

        var provider = new InMemoryMeasurementProvider(dfldHtml);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(
            at_11_00,
            TimeSpan.FromMinutes(2)
        );

        Assert.Equal(expectedMeasurements, measurements.ToList());
    }

    private readonly record struct MeasurementValue(string date, string time, double NoiseLevel);

    private class DfldHtml
    {
        private readonly List<MeasurementValue> _measurements = new();

        public DfldHtml(IEnumerable<MeasurementValue> measurements)
        {
            _measurements.AddRange(measurements);
        }

        public string Render()
        {
            var result = """
                <html>
                    <body>
                """;

            foreach (var measurement in _measurements)
            {
                var noiseLevel = measurement.NoiseLevel.ToString(
                    "F1",
                    CultureInfo.InvariantCulture
                );
                result += $"""
                            <area href="javascript:SetMultiParaUrl('form1','Z','{measurement.time}','ShowTrack.php?R=3&S=032&D={measurement.date}&N=900&Z={measurement.time}');"
                                  title="Flugspuren: {measurement.time}" />
                            <area title="Beschwerde zu {measurement.time} Uhr versenden [{noiseLevel} dBA]" />
                    """;
            }

            result += """
                    </body>
                </html>
                """;

            return result;
        }
    }
}
