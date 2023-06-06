using BlazorWasmApp.Models.Product;
using BlazorWasmApp.HttpServices.Abstracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorWasmApp.Tools;

namespace BlazorWasmApp.Pages
{
    public partial class Index:IDisposable
    {
        [Inject]
        private IProductHttpService ProductHttpService { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Inject]
        public HttpInterceptorService HttpInterceptorService { get; set; }

        private List<ProductUIDto> _productUIDtos = new List<ProductUIDto>();
        protected override async Task OnInitializedAsync()
        {
            HttpInterceptorService.RegisterInterceptEvent();

            //var product = await ProductHttpService.GetAsync(1);

            var productsDataResult = await ProductHttpService.GetAllAsync();
            if (productsDataResult==null)
            {
                await JSRuntime.InvokeVoidAsync("ShowToastrError", "Get product error");
                return;
            }

            if (!productsDataResult.IsSuccess)
            {
                await JSRuntime.InvokeVoidAsync("ShowToastrError", productsDataResult.Message);
                return;
            }


            _productUIDtos.Clear();
            _productUIDtos=productsDataResult.Data.ToList();

            await JSRuntime.InvokeVoidAsync("ShowToastrSuccess", _productUIDtos.Count);
        }

        public void Dispose()
        {
            HttpInterceptorService.DisposeInterceptEvent();
        }
    }
}
