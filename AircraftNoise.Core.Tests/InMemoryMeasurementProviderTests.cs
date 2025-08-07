using System.Globalization;
using AircraftNoise.Core.Adapters.Outbound;

namespace AircraftNoise.Core.Tests;

public class InMemoryMeasurementProviderTests
{
    // TODO: Test edge cases later
    [Fact]
    public async Task
        GetNoiseMeasurementsForPastTimePeriodAsync_RequestedEndTimeMatchesFirstMeasurement_ReturnsFirstMeasurement()
    {
        var expectedNoiseLevel = 55.0;
        var matchingDate = "31.12.2024";
        var matchingTime = "12:00:00";
        var expectedTimestampUtc = new DateTime(2024, 12, 31, 11, 0, 0, 0, 0, DateTimeKind.Utc);

        var measurementHtml = RenderHtmlAreasForMeasurement(expectedNoiseLevel, matchingDate, matchingTime); 

        var dfldHtmlResponse = $"""
                                <html>
                                    <body>
                                {measurementHtml}
                                    </body>
                                </html>
                                """;

        var provider = new InMemoryMeasurementProvider(dfldHtmlResponse);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(expectedTimestampUtc, TimeSpan.Zero);

        var measurementList = measurements.ToList();
        Assert.Single(measurementList);
        var firstMeasurement = measurementList.First();
        Assert.Equal(expectedTimestampUtc, firstMeasurement.TimestampUtc);
        Assert.Equal(expectedNoiseLevel, firstMeasurement.NoiseMeasurementDba);
    }

    [Fact]
    public async Task
        GetNoiseMeasurementsForPastTimePeriodAsync_RequestedEndTimeMatchesLastMeasurement_ReturnsLastMeasurement()
    {
        var expectedNoiseLevel = 55.0;
        var matchingDate = "31.12.2024";
        var matchingTime = "12:00:00";
        var expectedTimestampUtc = new DateTime(2024, 12, 31, 11, 0, 0, 0, 0, DateTimeKind.Utc);

        var otherTime = "11:00:00";
        var otherNoiseLevel = 60.0;

        var firstMeasurementHtml = RenderHtmlAreasForMeasurement(otherNoiseLevel, matchingDate, otherTime);
        var secondMeasurementHtml = RenderHtmlAreasForMeasurement(expectedNoiseLevel, matchingDate, matchingTime);

        var dfldHtmlResponse = $"""
                                <html>
                                    <body>
                                {firstMeasurementHtml}
                                {secondMeasurementHtml}
                                    </body>
                                </html>
                                """;

        var provider = new InMemoryMeasurementProvider(dfldHtmlResponse);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(expectedTimestampUtc, TimeSpan.Zero);

        var measurementList = measurements.ToList();
        Assert.Single(measurementList);
        var firstMeasurement = measurementList.First();
        Assert.Equal(expectedTimestampUtc, firstMeasurement.TimestampUtc);
        Assert.Equal(expectedNoiseLevel, firstMeasurement.NoiseMeasurementDba);
    }

    [Fact]
    public async Task
        GetNoiseMeasurementsForPastTimePeriodAsync_RequestedEndTimeMatchesRange_ReturnsMeasurementList()
    {
        var expectedNoiseLevels = new List<double> { 42.0, 43.0, 44.0 };
        var matchingDate = "31.12.2024";
        var matchingTimes = new List<string> { "11:58:00", "11:59:00", "12:00:00" };
        var expectedTimestampsUtc = new List<DateTime> {
            new DateTime(2024, 12, 31, 10, 58, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 12, 31, 10, 59, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 12, 31, 11, 0, 0, 0, 0, DateTimeKind.Utc),
        };
        var otherNoiseLevel = 60.0;
        var otherTime = "10:00:00";

        var firstMeasurementHtml = RenderHtmlAreasForMeasurement(otherNoiseLevel, matchingDate, otherTime);
        var htmlForExpectedMeasurementAreas = expectedNoiseLevels.Zip(matchingTimes).Select(x => RenderHtmlAreasForMeasurement(x.First, matchingDate, x.Second)).ToList();

        var dfldHtmlResponse = $"""
                                <html>
                                    <body>
                                {firstMeasurementHtml}
                                {string.Join("\n", htmlForExpectedMeasurementAreas)}
                                    </body>
                                </html>
                                """;

        var provider = new InMemoryMeasurementProvider(dfldHtmlResponse);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(expectedTimestampsUtc.Last(), TimeSpan.FromMinutes(2));

        var measurementList = measurements.ToList();
        var actualTimestampsUtc = measurementList.Select(x => x.TimestampUtc).ToList();
        Assert.Equal(expectedTimestampsUtc, actualTimestampsUtc);
    }

    private static string RenderHtmlAreasForMeasurement(double configuredNoiseLevel, string configuredPeakDate, string configuredPeakTime)
    {
        var noiseLevel = configuredNoiseLevel.ToString("F1", CultureInfo.InvariantCulture);

        var HtmlAreas = $"""
                                 <area href="javascript:SetMultiParaUrl('form1','Z','{configuredPeakTime}','ShowTrack.php?R=3&S=032&D={configuredPeakDate}&N=900&Z={configuredPeakTime}');"
                                       title="Flugspuren: {configuredPeakTime}" />
                                 <area title="Beschwerde zu {configuredPeakTime} Uhr versenden [{noiseLevel} dBA]" />
                         """;
        return HtmlAreas;
    }
}
