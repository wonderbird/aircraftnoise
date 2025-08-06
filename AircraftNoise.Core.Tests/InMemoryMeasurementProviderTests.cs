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
        var configuredNoiseLevel = 55.0;
        var configuredPeakTimestampUtcString = "2024-12-31T11:00:00Z";
        var configuredPeakTimestampUtc = DateTime.Parse(configuredPeakTimestampUtcString, CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal);

        var measurementHtml = RenderHtmlAreasForMeasurement(configuredNoiseLevel, configuredPeakTimestampUtcString);

        var dfldHtmlResponse = $"""
                                <html>
                                    <body>
                                {measurementHtml}
                                    </body>
                                </html>
                                """;

        var provider = new InMemoryMeasurementProvider(dfldHtmlResponse);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(configuredPeakTimestampUtc, TimeSpan.Zero);

        var firstMeasurement = measurements.First();
        var firstMeasurementTimestamp =
            firstMeasurement.TimestampUtc.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        Assert.Equal(configuredPeakTimestampUtcString, firstMeasurementTimestamp);
        Assert.Equal(configuredNoiseLevel, firstMeasurement.NoiseMeasurementDba);
    }
    
    [Fact]
    public async Task
        GetNoiseMeasurementsForPastTimePeriodAsync_RequestedEndTimeMatchesLastMeasurement_ReturnsLastMeasurement()
    {
        var expectedNoiseLevel = 55.0;
        var expectedTimestampUtcString = "2024-12-31T11:00:00Z";
        var expectedTimestampUtc = DateTime.Parse(expectedTimestampUtcString, CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal);
        var anyNoiseLevelExceptExpected = 60.0;
        var anyTimestampUtcBeforeExpected = "2024-12-31T10:00:00Z";
        
        var firstMeasurementHtml = RenderHtmlAreasForMeasurement(anyNoiseLevelExceptExpected, anyTimestampUtcBeforeExpected);
        var secondMeasurementHtml = RenderHtmlAreasForMeasurement(expectedNoiseLevel, expectedTimestampUtcString);

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
        var firstMeasurementTimestamp =
            firstMeasurement.TimestampUtc.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        Assert.Equal(expectedTimestampUtcString, firstMeasurementTimestamp);
        Assert.Equal(expectedNoiseLevel, firstMeasurement.NoiseMeasurementDba);
    }

    private static string RenderHtmlAreasForMeasurement(double configuredNoiseLevel, string configuredPeakTimestamp)
    {
        var noiseLevel = configuredNoiseLevel.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);

        // Avoid time zone issues when running tests on different systems.
        //
        // To have a system and location independent test, the timestamp is given in UTC time.
        // The DFLD response is always in the W. Europe Standard Time zone.
        // This procedure avoids problems, if a developer runs the test in a different time zone.

        // Set up peak time on 2024-12-31 at 12:00:00 CET (UTC+01:00).
        var peakTimestampUtc = DateTime.Parse(configuredPeakTimestamp, CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal);
        var timeZoneCet = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        var peakTimestampCet = TimeZoneInfo.ConvertTimeFromUtc(peakTimestampUtc, timeZoneCet);
        var peakTime = peakTimestampCet.ToString("HH:mm:ss");
        var peakDate = peakTimestampCet.ToString("dd.MM.yyyy");

        var HtmlAreas = $"""
                                 <area href="javascript:SetMultiParaUrl('form1','Z','{peakTime}','ShowTrack.php?R=3&S=032&D={peakDate}&N=900&Z={peakTime}');"
                                       title="Flugspuren: {peakTime}" />
                                 <area title="Beschwerde zu {peakTime} Uhr versenden [{noiseLevel} dBA]" />
                         """;
        return HtmlAreas;
    }
}