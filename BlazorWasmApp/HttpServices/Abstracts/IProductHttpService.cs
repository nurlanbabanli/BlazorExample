using BlazorWasmApp.Models.Product;
using BlazorWasmApp.Results.Abstract;

namespace BlazorWasmApp.HttpServices.Abstracts
{
    internal interface IProductHttpService
    {
        Task<IDataResult<ProductUIDto>> GetAsync(int productId);
        Task<IDataResult<IEnumerable<ProductUIDto>>> GetAllAsync();
    }
}
