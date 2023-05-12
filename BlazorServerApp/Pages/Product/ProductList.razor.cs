using BlazorServerApp.Models.Product;
using Business.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorServerApp.Pages.Product
{
    public partial class ProductList:ComponentBase
    {
        [Inject]
        private IProductService _productService { get; set; }
        [Inject]
        private IJSRuntime _jSRuntime { get; set; }

        private List<ProductUIDto> productUIDtos = new List<ProductUIDto>();
        private bool isLoading = false;
        private int productOrderNo=0;
        private int productToDeleteId = 0;
        private ProductUIDto productUIToDelete;
        private bool isDeleteProcessing = false;
        protected override async Task OnInitializedAsync()
        {
            await Task.Run(LoadProducts);
            //await LoadProducts();
        }

        private async Task LoadProducts()
        {
            productUIDtos.Clear();
            isLoading = true;
            var dataResultProductDto=await _productService.GetAllAsync();
            if(dataResultProductDto == null)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastrError", "Get product error");
            }
            else if (!dataResultProductDto.IsSuccess)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastrError", dataResultProductDto.Message);
            }

            foreach (var product in dataResultProductDto.Data)
            {
                productUIDtos.Add(new ProductUIDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description= product.Description,
                    Color = product.Color,
                    ImageUrl = product.ImageUrl,
                    ShopFavorites = product.ShopFavorites,
                    CategoryId = product.CategoryId,
                    CategoryName= product.CategoryName,
                });
            }

            isLoading = false;
        }

        private async Task HandleDelete(int productId)
        {
            productToDeleteId=productId;
            productUIToDelete=productUIDtos.FirstOrDefault(p=>p.Id==productId);
            await _jSRuntime.InvokeVoidAsync("ShowDeleteConfirmation");
        }

        private async Task HandleDeleteConfirm(bool isDeleteConfirmed)
        {
            isDeleteProcessing=true;
            if(isDeleteConfirmed && productToDeleteId!=0)
            {
                await DeleteProduct();
                await _jSRuntime.InvokeVoidAsync("HideDeleteConfirmation");
            }
            isDeleteProcessing=false;
        }

        private async Task DeleteProduct()
        {
            var deleteResult=await _productService.DeleteAsync(productToDeleteId);
            if (deleteResult==null)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastrError", "Delete product error");
            }
            else if (!deleteResult.IsSuccess)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastrError", deleteResult.Message);
            }
            else
            {
                if (productUIToDelete!=null)
                {
                    await _jSRuntime.InvokeVoidAsync("ShowToastrSuccess", "Product deleted"+"<br>"+"Category Name: "+productUIToDelete.Name);
                }
                else
                {
                    await _jSRuntime.InvokeVoidAsync("ShowToastrSuccess", "Product deleted");
                }
            }

            await LoadProducts();
        }
    }
}
