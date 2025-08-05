using AircraftNoise.Core.Domain;
using HtmlAgilityPack;

namespace AircraftNoise.Core.Adapters.Outbound;

public class InMemoryMeasurementProvider : ICanProvideMeasurements
{
    private readonly string _dfldHtmlResponse;
    private readonly TimeZoneInfo _timeZoneCet = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

    private readonly record struct HtmlAreaElement(int Index, string Title, string Href)
    {
        public static HtmlAreaElement Parse(HtmlNode x, int index)
        {
            var title = x.GetAttributeValue("title", string.Empty);
            var href = x.GetAttributeValue("href", string.Empty);
            
            // TODO: Throw an exception if title or href is empty (create a test first)
            
            return new HtmlAreaElement(index, title, href);
        }
    }

    private readonly record struct Measurement(string Subject, string TraceScript, double NoiseLevel)
    {
        public static Measurement Parse(int _, IEnumerable<HtmlAreaElement> areas) => Parse(areas);

        private static Measurement Parse(IEnumerable<HtmlAreaElement> areas)
        {
            var areaList = areas.ToList();
            
            // TODO: Assert that there are always two areas in the list.
            var subject = areaList.Last().Title;
            var noiseLevelMatch =
                System.Text.RegularExpressions.Regex.Match(subject, @"(\d+(\.\d+)?) dBA");
            var noiseLevel = double.Parse(noiseLevelMatch.Groups[1].Value,
                System.Globalization.CultureInfo.InvariantCulture);
            
            var traceScript = areaList.First().Href;
            return new Measurement(subject, traceScript, noiseLevel);
        }
    }
    
    public InMemoryMeasurementProvider(string dfldHtmlResponse)
    {
        _dfldHtmlResponse = dfldHtmlResponse;
    }

    public Task<IEnumerable<NoiseMeasurement>> GetNoiseMeasurementsForPastTimePeriodAsync(DateTime end, TimeSpan duration)
    {
        var html = new HtmlDocument();
        html.LoadHtml(_dfldHtmlResponse);
        
        var areaNodes = html.DocumentNode.SelectNodes("//area");

        // TODO: What about moving the regular expressions from below into the Complaint parser?
        var measurements = areaNodes.Select(HtmlAreaElement.Parse)
            .GroupBy(x => x.Index / 2, x => x, Measurement.Parse)
            .ToList();

        var result = measurements.Select(complaint =>
        {
            var noiseLevelMatch =
                System.Text.RegularExpressions.Regex.Match(complaint.Subject, @"(\d+(\.\d+)?) dBA");
            var noiseLevel = double.Parse(noiseLevelMatch.Groups[1].Value,
                System.Globalization.CultureInfo.InvariantCulture);

            var dateMatch = System.Text.RegularExpressions.Regex.Match(complaint.TraceScript, @"(\d{2}\.\d{2}\.\d{4})");
            var timeMatch = System.Text.RegularExpressions.Regex.Match(complaint.TraceScript, @"(\d{2}:\d{2}:\d{2})");
            var timestampCet = DateTime.ParseExact(dateMatch.Groups[1].Value, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture)
                .Add(TimeSpan.ParseExact(timeMatch.Groups[1].Value, "hh\\:mm\\:ss",
                    System.Globalization.CultureInfo.InvariantCulture));
            var timestampUtc = TimeZoneInfo.ConvertTimeToUtc(timestampCet, _timeZoneCet);

            return new NoiseMeasurement(timestampUtc, noiseLevel);
        });

        return Task.FromResult(result);
    }
}