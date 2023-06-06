using BlazorWasmApp.Models.Product;
using BlazorWasmApp.Results.Abstract;
using BlazorWasmApp.Results.Concrete;
using BlazorWasmApp.HttpServices.Abstracts;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWasmApp.Services.Concretes
{
    internal class ProductHttpManager : IProductHttpService
    {
        private readonly HttpClient _httpClient;

        public ProductHttpManager(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }

        public async Task<IDataResult<IEnumerable<ProductUIDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("/api/Product/getAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content==null) return new ErrorDataResult<IEnumerable<ProductUIDto>>(null, "Get prudct error");

                try
                {
                    var productsDataResult = JsonConvert.DeserializeObject<IEnumerable<ProductUIDto>>(content);
                    if (productsDataResult==null) return new ErrorDataResult<IEnumerable<ProductUIDto>>(null, "Get prudct error");
                    return new SuccessDataResult<IEnumerable<ProductUIDto>>(productsDataResult);
                }
                catch (Exception e)
                {
                    return new ErrorDataResult<IEnumerable<ProductUIDto>>(null, "Get prudct error: "+e.Message);
                }
            }
            else
            {
                return new ErrorDataResult<IEnumerable<ProductUIDto>>(null, "Get product error: Status code:"+ response.StatusCode.ToString());
            }

        }

        public async Task<IDataResult<ProductUIDto>> GetAsync(int productId)
        {
            var requestModel=new ProductRequestByIdModel { ProductId = productId };
            var jsonRequestModel=JsonConvert.SerializeObject(requestModel);

            var requestContent = new StringContent(jsonRequestModel, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Product/getProductById", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content==null) return new ErrorDataResult<ProductUIDto>(null, "Get prudct error");

                try
                {
                    var productDataResult=JsonConvert.DeserializeObject<ProductUIDto>(content);
                    if(productDataResult==null) return new ErrorDataResult<ProductUIDto>(null, "Get prudct error");
                    return new SuccessDataResult<ProductUIDto>(productDataResult);
                }
                catch (Exception e)
                {
                    return new ErrorDataResult<ProductUIDto>(null, "Get prudct error: "+e.Message);
                }
            }
            else
            {
                return new ErrorDataResult<ProductUIDto>(null, "Get product error: Status code:"+ response.StatusCode.ToString());
            }
        }
    }
}
