using AircraftNoise.Core.Adapters.Outbound;
using AircraftNoise.Core.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AircraftNoise.Web.Pages;

public class IndexModel : PageModel
{
    public Region Region { get; set; }
    public MeasurementStation MeasurementStation { get; set; }

    private readonly ILogger<IndexModel> _logger;
    private readonly ICanFindLocation _locationFinder;
    private readonly ICanFindRegion _regionFinder;
    private readonly ICanFindMeasurementStation _measurementStationFinder;

    public IndexModel(
        ILogger<IndexModel> logger,
        ICanFindLocation locationFinder,
        ICanFindRegion regionFinder,
        ICanFindMeasurementStation measurementStationFinder
    )
    {
        _logger = logger;
        _locationFinder = locationFinder;
        _regionFinder = regionFinder;
        _measurementStationFinder = measurementStationFinder;
    }

    public void OnGet()
    {
        var location = _locationFinder.FindLocation(HttpContext.Connection.RemoteIpAddress);
        Region = _regionFinder.FindRegion(location);
        MeasurementStation = _measurementStationFinder.FindMeasurementStation(location);
    }
}
