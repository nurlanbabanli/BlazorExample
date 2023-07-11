using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace BlazorWasmApp.Tools
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _httpClientInterceptor;
        private readonly NavigationManager _navigationManager;
        public HttpInterceptorService(HttpClientInterceptor httpClientInterceptor, NavigationManager navigationManager)
        {
            _httpClientInterceptor=httpClientInterceptor;
            _navigationManager=navigationManager;
        }

        public void RegisterInterceptEvent()=> _httpClientInterceptor.AfterSend+=InterceptResponse;
        public void DisposeInterceptEvent()=>_httpClientInterceptor.AfterSend-=InterceptResponse;

        private void InterceptResponse(object? sender, HttpClientInterceptorEventArgs e)
        {
            string message=string.Empty;

            if (e.Response != null)
            {
                
                if (!e.Response.IsSuccessStatusCode)
                {
                    var statusCode=e.Response.StatusCode;
                    if (statusCode==HttpStatusCode.NotFound)
                    {
                        message = "The requested resorce was not found.";
                        _navigationManager.NavigateTo("/404/"+message);

                        //throw new Exception(message);
                        return;
                    }

                    if (statusCode==HttpStatusCode.Unauthorized)
                    {
                        message = "User is not authorized";
                        _navigationManager.NavigateTo("/unauthorized");

                        //throw new Exception(message);
                        return;
                    }

                    _navigationManager.NavigateTo("/500");
                }
            }
            else
            {
                _navigationManager.NavigateTo("/404/"+message);
                return;
            }
        }
    }
}
