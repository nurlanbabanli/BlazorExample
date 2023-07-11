using BlazorWasmApp.HttpServices.Abstracts;
using BlazorWasmApp.HttpServices.Concretes;
using BlazorWasmApp.Services.Concretes;

namespace BlazorWasmApp.Extensions
{
    internal static class HttpServiceExtensions
    {
        internal static void AddHttpServices(this IServiceCollection services)
        {
            services.AddTransient<IProductHttpService, ProductHttpManager>();
            services.AddTransient<IHttpRepository, HttpRepository>();
            services.AddTransient<IAuthenticationHttpService, AuthenticationHttpService>();
        }
    }
}
