using AircraftNoise.Core.Domain;
using HtmlAgilityPack;

namespace AircraftNoise.Core.Adapters.Outbound;

public class InMemoryMeasurementProvider : ICanProvideMeasurements
{
    private readonly record struct HtmlAreaElement(int Index, string Title, string Href);

    private static readonly TimeZoneInfo TimeZoneCet = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

    private readonly string _dfldHtmlResponse;

    public InMemoryMeasurementProvider(string dfldHtmlResponse)
    {
        _dfldHtmlResponse = dfldHtmlResponse;
    }

    public Task<IEnumerable<NoiseMeasurement>> GetNoiseMeasurementsForPastTimePeriodAsync(DateTime endTimeUtc,
        TimeSpan duration)
    {
        var html = new HtmlDocument();
        html.LoadHtml(_dfldHtmlResponse);

        var areaNodes = html.DocumentNode.SelectNodes("//area");

        var result = areaNodes.Select(ParseHtmlAreaElement)
            .GroupBy(x => x.Index / 2, x => x, ParseNoiseMeasurement)
            .Where(x => x.TimestampUtc == endTimeUtc);

        return Task.FromResult(result);
    }

    private static HtmlAreaElement ParseHtmlAreaElement(HtmlNode x, int index)
    {
        var title = x.GetAttributeValue("title", string.Empty);
        var href = x.GetAttributeValue("href", string.Empty);

        // TODO(validate input data): Throw an exception if title or href is empty (create a test first)

        return new HtmlAreaElement(index, title, href);
    }

    private static NoiseMeasurement ParseNoiseMeasurement(int index, IEnumerable<HtmlAreaElement> areas)
    {
        var areaList = areas.ToList();

        // TODO(validate input data): Assert that there are always two areas in the list.

        var noiseLevel = ParseNoiseLevel(areaList.Last().Title);
        var timestampUtc = ParseTimestampUtc(areaList.First().Href);

        return new NoiseMeasurement(timestampUtc, noiseLevel);
    }

    private static double ParseNoiseLevel(string titleAttribute)
    {
        var match = System.Text.RegularExpressions.Regex.Match(titleAttribute, @"(\d+(\.\d+)?) dBA");
        var result = double.Parse(match.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture);
        return result;
    }

    private static DateTime ParseTimestampUtc(string traceScript)
    {
        var dateMatch = System.Text.RegularExpressions.Regex.Match(traceScript, @"(\d{2}\.\d{2}\.\d{4})");
        var timeMatch = System.Text.RegularExpressions.Regex.Match(traceScript, @"(\d{2}:\d{2}:\d{2})");
        var timestampCet = DateTime.ParseExact(dateMatch.Groups[1].Value, "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture)
            .Add(TimeSpan.ParseExact(timeMatch.Groups[1].Value, "hh\\:mm\\:ss",
                System.Globalization.CultureInfo.InvariantCulture));
        var timestampUtc = TimeZoneInfo.ConvertTimeToUtc(timestampCet, TimeZoneCet);
        return timestampUtc;
    }
}