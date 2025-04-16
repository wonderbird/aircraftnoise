using System.Globalization;
using System.Text.RegularExpressions;
using AircraftNoise.Core.Domain;
using HtmlAgilityPack;

namespace AircraftNoise.Core.Adapters.Outbound;

public class MeasurementFileReader : ICanProvideMeasurements
{
    private readonly string _filePath;
    private static readonly Regex NoiseRegex = new Regex(@"Beschwerde zu (\d{2}:\d{2}:\d{2}) Uhr versenden \[(\d+\.\d+) dBA", RegexOptions.Compiled);

    public MeasurementFileReader(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<IEnumerable<Domain.NoiseMeasurement>> GetNoiseMeasurementsForPastTimePeriodAsync(DateTime end,
        TimeSpan duration)
    {
        var measurements = new List<Domain.NoiseMeasurement>();

        if (!File.Exists(_filePath))
            return measurements;

        try
        {
            var content = await File.ReadAllTextAsync(_filePath);
            measurements = ParseHtmlData(content);
            
            // Calculate the start time of the interval
            DateTime start = end - duration;
            
            // Filter measurements based on the time interval
            measurements = measurements
                .Where(m => m.Timestamp >= start && m.Timestamp <= end)
                .ToList();
        }
        catch (Exception ex)
        {
            // Consider proper exception handling or logging here
            Console.WriteLine($"Error reading measurements: {ex.Message}");
        }

        return measurements;
    }
    
    private List<NoiseMeasurement> ParseHtmlData(string htmlContent)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);
        
        var areaElements = doc.DocumentNode.SelectNodes("//area[contains(@title, 'Beschwerde zu')]");
        if (areaElements == null)
            return new List<NoiseMeasurement>();
            
        var dataPoints = new List<NoiseMeasurement>();
        
        foreach (var area in areaElements)
        {
            var title = area.GetAttributeValue("title", string.Empty);
            var href = area.GetAttributeValue("href", string.Empty);
            var match = NoiseRegex.Match(title);
            
            if (match.Success)
            {
                var timeString = match.Groups[1].Value;
                var noiseLevelString = match.Groups[2].Value;
                
                // Extract date from the href attribute - D parameter
                DateTime baseDate = DateTime.MinValue;
                var dateMatch = Regex.Match(href, @"D=(\d{2}\.\d{2}\.\d{4})");
                if (dateMatch.Success)
                {
                    var dateString = dateMatch.Groups[1].Value;
                    DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, 
                        DateTimeStyles.None, out baseDate);
                }
                
                if (TimeSpan.TryParse(timeString, out var timeSpan) && 
                    double.TryParse(noiseLevelString, NumberStyles.Any, CultureInfo.InvariantCulture, out var noiseLevel) &&
                    baseDate != DateTime.MinValue)
                {
                    var timestamp = baseDate.Add(timeSpan);
                    dataPoints.Add(new NoiseMeasurement(timestamp, noiseLevel));
                }
            }
        }
        
        return dataPoints.OrderBy(dp => dp.Timestamp).ToList();
    }
}
