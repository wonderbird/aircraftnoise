using AircraftNoise.Core.Domain;
using HtmlAgilityPack;

namespace AircraftNoise.Core.Adapters.Outbound;

public class InMemoryMeasurementProvider : ICanProvideMeasurements
{
    private readonly string _dfldHtmlResponse;
    private readonly TimeZoneInfo _timeZoneCet = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

    private readonly record struct AreaPayload(int Index, string Title, string Href)
    {
        public static AreaPayload Parse(HtmlNode x, int index)
        {
            var title = x.GetAttributeValue("title", string.Empty);
            var href = x.GetAttributeValue("href", string.Empty);
            
            // TODO: Throw an exception if title or href is empty (create a test first)
            
            return new AreaPayload(index, title, href);
        }
    }

    private readonly record struct Complaint(string Subject, string TraceScript)
    {
        public static Complaint Parse(IEnumerable<AreaPayload> areas)
        {
            // TODO: Assert that there are always two areas in the list.

            var areaList = areas.ToList();
            var subject = areaList.Last().Title;
            var traceScript = areaList.First().Href;
            return new Complaint(subject, traceScript);
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

        var complaints = areaNodes.Select(AreaPayload.Parse)
            .GroupBy(x => x.Index / 2, x => x, (_, areas) =>
            {
                // TODO: Hemingway Bridge - Move this to another parser; what about moving the regular expressions from below into that parser?
                var areaList = areas.ToList();
                var subject = areaList.Last().Title;
                var traceScript = areaList.First().Href;
                return new Complaint(subject, traceScript);
            })
            .ToList();

        var result = complaints.Select(complaint =>
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