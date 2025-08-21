using AircraftNoise.Core.Adapters.Outbound;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

var dfldHtmlResponse = File.ReadAllText(
    Path.Combine(AppContext.BaseDirectory, "Data", "measurements.html")
);
builder.Services.AddSingleton<ICanProvideMeasurements, InMemoryMeasurementProvider>(
    _ => new InMemoryMeasurementProvider(dfldHtmlResponse)
);
builder.Services.AddSingleton<ICanFindLocation, LocationLookupService>();
builder.Services.AddSingleton<ICanFindRegion, RegionRepository>();
builder.Services.AddSingleton<ICanFindMeasurementStation, MeasurementStationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

// Make the Program class accessible for testing
public partial class Program { }
