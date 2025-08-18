using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AircraftNoise.Web.Pages;

public class ImprintModel : PageModel
{
    private readonly ILogger<ImprintModel> _logger;

    public ImprintModel(ILogger<ImprintModel> logger)
    {
        _logger = logger;
    }

    public void OnGet() { }
}
