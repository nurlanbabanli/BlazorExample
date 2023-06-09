using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlazorServerApp.Data;
using BlazorServerApp.Service.Abstract;
using BlazorServerApp.Service.Concrete;
using Business.DependencyResolvers;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.IoC;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<KestrelServerOptions>(configOptions =>
//{
//    configOptions.Limits.MaxRequestBodySize=10485760;
//});

//builder.Services.Configure<FormOptions>(options =>
//{
//    options.
//});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<IFileUploadService, FileUploadManager>();

builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
