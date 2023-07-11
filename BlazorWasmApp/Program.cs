using BlazorWasmApp;
using BlazorWasmApp.AuthProviders;
using BlazorWasmApp.Extensions;
using BlazorWasmApp.HttpServices.Abstracts;
using BlazorWasmApp.Services.Concretes;
using BlazorWasmApp.Tools;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44327") }.EnableIntercept(sp));
builder.Services.AddAuthorizationCore();

//builder.Services.AddHttpClient()

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddScoped<AuthenticationStateProvider, ProjectAuthStateProvider>();


//builder.Services.AddTransient<IProductHttpService, ProductHttpManager>();
builder.Services.AddHttpServices();

await builder.Build().RunAsync();
