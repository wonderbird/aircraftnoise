using AircraftNoise.Web.Adapters.Outbound;
using AircraftNoise.Web.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AircraftNoise.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public Region Region { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var location = new LocationLookupService().GetLocation(HttpContext.Connection.RemoteIpAddress);
        Region = new RegionLookupService().GetRegion(location);
    }
}
