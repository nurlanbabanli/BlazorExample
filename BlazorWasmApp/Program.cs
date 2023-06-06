using BlazorWasmApp;
using BlazorWasmApp.HttpServices.Abstracts;
using BlazorWasmApp.Services.Concretes;
using BlazorWasmApp.Tools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44327") }.EnableIntercept(sp));


//builder.Services.AddHttpClient()

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddTransient<IProductHttpService, ProductHttpManager>();

await builder.Build().RunAsync();
