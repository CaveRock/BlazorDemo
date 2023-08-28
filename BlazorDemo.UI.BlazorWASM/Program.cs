using BlazorDemo.UI.BlazorWASM;
using BlazorDemo.UI.BlazorWASM.Code;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DemoApp>();

builder.Services.AddHttpClient("api", client => {
    if (builder.Configuration.GetSection("Api")["ApiBase"] ==  null)
    {
        throw new ConfigurationErrorsException("ApiUrl missing from config");
    } else
    {
        client.BaseAddress = new Uri(builder.Configuration.GetSection("Api")["ApiBase"]);
    }
});

await builder.Build().RunAsync();
