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
        var configuredPeakTimestamp = "2024-12-31T11:00:00Z";

        var measurementHtml = RenderHtmlAreasForMeasurement(configuredNoiseLevel, configuredPeakTimestamp);

        var dfldHtmlResponse = $"""
                                <html>
                                    <body>
                                {measurementHtml}
                                    </body>
                                </html>
                                """;

        var provider = new InMemoryMeasurementProvider(dfldHtmlResponse);

        var measurements = await provider.GetNoiseMeasurementsForPastTimePeriodAsync(DateTime.MinValue, TimeSpan.Zero);

        var firstMeasurement = measurements.First();
        var firstMeasurementTimestamp = firstMeasurement.Timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        Assert.Equal(configuredPeakTimestamp, firstMeasurementTimestamp);
        Assert.Equal(configuredNoiseLevel, firstMeasurement.NoiseMeasurementDba);
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