using BlazorServerApp.Models.Category;
using BlazorServerApp.Models.Product;
using BlazorServerApp.Service.Abstract;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorServerApp.Pages.Product
{
    public partial class ProductEdit : ComponentBase
    {
        [Inject]
        private ICategoryService _categoryService { get; set; }
        [Inject]
        private IJSRuntime _jsRuntime { get; set; }
        [Inject]
        private IProductService _productService { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }
        [Inject]
        IFileUploadService _fileUploadService { get; set; }

        [Parameter]
        public int Id { get; set; }

        private ProductUIDto productUIDto = new ProductUIDto()
        {
            ImageUrl="/images/defaultImage.jpg"
        };
        private List<CategoryUIDto> categoryUIDtos = new List<CategoryUIDto>();
        private string Title = "Create";
        private string productOldImageUrl = "";
        private string productDefaultImageUrl = "/images/defaultImage.jpg";
        private bool productImageUpdatePending=false;

        protected override async Task OnInitializedAsync()
        {
            if (Id!=0)
            {
                Title="Update";
                await GetProduct(Id);
            }

            await LoadCategories();
        }

        private async Task HandleValidSubmit(EditContext editContext)
        {
            if (productUIDto.Id==0)
            {
                await AddProduct(editContext);
            }
            else
            {
                await UpdateProduct();
            }

            productImageUpdatePending = false;
        }


        private async Task HandleImageUpload(InputFileChangeEventArgs inputFileChangeEventArgs)
        {
            try
            {
               await EditCancelHandler();

                var selectedFileList = inputFileChangeEventArgs.GetMultipleFiles();

                if (selectedFileList.Count>0)
                {

                    FileInfo fileInfo = new FileInfo(selectedFileList[0].Name);
                    if (fileInfo.Extension.ToLower()==".jpg" || fileInfo.Extension.ToLower()==".png" || fileInfo.Extension.ToLower()==".jpeg")
                    {
                        var imageUrlDataResult = await _fileUploadService.UploadFile(selectedFileList[0]);
                        if (imageUrlDataResult==null)
                        {
                            await _jsRuntime.InvokeVoidAsync("ShowToastrError", "Product image upload error");
                        }
                        else if (!imageUrlDataResult.IsSuccess)
                        {
                            await _jsRuntime.InvokeVoidAsync("ShowToastrError", imageUrlDataResult.Message);
                        }

                        productOldImageUrl=productUIDto.ImageUrl;
                        productUIDto.ImageUrl=imageUrlDataResult.Data;
                        productImageUpdatePending=true;
                    }
                    else
                    {
                        await _jsRuntime.InvokeVoidAsync("ShowToastrError", "Please select .jpg/ .jpeg/ .png  file format only");
                        return;
                    }
                }
            }
            catch (Exception e)
            {

                await _jsRuntime.InvokeVoidAsync("ShowToastrError", e.Message);
            }
        }

        private async Task EditCancelHandler()
        {
            if(productImageUpdatePending && productUIDto.ImageUrl!=productDefaultImageUrl)
            {
                await _fileUploadService.DeleteFile(productUIDto.ImageUrl);
                productImageUpdatePending = false;
            }
        }

        private async Task UpdateProduct()
        {
            var productDto = new ProductDto();
            productDto.Id=productUIDto.Id;
            productDto.Name=productUIDto.Name;
            productDto.Color=productUIDto.Color;
            productDto.Description=productUIDto.Description;
            productDto.ShopFavorites=productUIDto.ShopFavorites;
            productDto.CategoryId=productUIDto.CategoryId;
            productDto.ImageUrl=productUIDto.ImageUrl;

            var updateDataResult = await _productService.UpdateAsync(productDto);
            if (updateDataResult==null)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", "Product update error");
            }
            else if (!updateDataResult.IsSuccess)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", updateDataResult.Message);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrSuccess", "Product updated");
                _navigationManager.NavigateTo("/product");
            }

            if(productOldImageUrl!=productUIDto.ImageUrl && productOldImageUrl!=productDefaultImageUrl)
            {
                await _fileUploadService.DeleteFile(productOldImageUrl);
            }
        }

        private async Task GetProduct(int id)
        {
            var getProductDataResult = await _productService.GetAsync(id);
            if (getProductDataResult==null)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", "Get Product error");
                return;
            }
            else if (!getProductDataResult.IsSuccess)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", getProductDataResult.Message);
                return;
            }

            productUIDto.Id = getProductDataResult.Data.Id;
            productUIDto.Name= getProductDataResult.Data.Name;
            productUIDto.Color= getProductDataResult.Data.Color;
            productUIDto.Description= getProductDataResult.Data.Description;
            productUIDto.ShopFavorites=getProductDataResult.Data.ShopFavorites;
            productUIDto.CategoryId= getProductDataResult.Data.CategoryId;
            if (getProductDataResult.Data.ImageUrl!=null)
            {
                productUIDto.ImageUrl=getProductDataResult.Data.ImageUrl;
            }
        }

        private async Task AddProduct(EditContext editContext)
        {
            var productDto = new ProductDto();
            productDto.Name=productUIDto.Name;
            productDto.Color=productUIDto.Color;
            productDto.Description=productUIDto.Description;
            productDto.ShopFavorites=productUIDto.ShopFavorites;
            productDto.CategoryId=productUIDto.CategoryId;

            var addResult = await _productService.AddAsync(productDto);
            if (addResult==null)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", "Product add error");
            }
            else if (!addResult.IsSuccess)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", addResult.Message);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrSuccess", "Product added");
                _navigationManager.NavigateTo("/product");
            }
        }

        private async Task LoadCategories()
        {
            categoryUIDtos.Clear();

            var dataResultCategoryDto = await _categoryService.GetAllAsync();
            if (dataResultCategoryDto==null)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", "Get category error");
            }
            else if (!dataResultCategoryDto.IsSuccess)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", dataResultCategoryDto.Message);
            }

            var tempCategoryDtos = dataResultCategoryDto.Data.OrderByDescending(x => x.CreatedDate).ToList();

            foreach (var category in tempCategoryDtos)
            {
                categoryUIDtos.Add(
                    new CategoryUIDto
                    {
                        Id=category.Id,
                        Name=category.Name,
                        CreatedDate=category.CreatedDate
                    });
            }
        }
    }
}
