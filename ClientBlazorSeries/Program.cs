using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientBlazorSeries;
using ClientBlazorSeries.Services;
using ClientBlazorSeries.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7122/")
});


builder.Services.AddScoped<ISerieService, SerieService>();


builder.Services.AddScoped<SeriesViewModel>();
builder.Services.AddScoped<SerieDetailViewModel>();

await builder.Build().RunAsync();