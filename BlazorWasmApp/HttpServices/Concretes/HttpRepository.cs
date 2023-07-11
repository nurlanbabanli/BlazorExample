using BlazorWasmApp.HttpServices.Abstracts;
using BlazorWasmApp.Results.Abstract;
using BlazorWasmApp.Results.Concrete;
using Newtonsoft.Json;

namespace BlazorWasmApp.HttpServices.Concretes
{
    public class HttpRepository:IHttpRepository
    {
        private readonly HttpClient _httpClient;

        public HttpRepository(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }

        public async Task<IDataResult<TResult>> GetAsync<TResult>(string requestUrl)
        {
            try
            {
                var response=await _httpClient.GetAsync(requestUrl);
                if (response==null) return new ErrorDataResult<TResult>(default(TResult), "Get data error. Response is null. ");

                if (response.IsSuccessStatusCode)
                {
                    var content=await response.Content.ReadAsStringAsync();
                    if (content==null) return new ErrorDataResult<TResult>(default(TResult), "Get data error. Content is null. ");

                    var result=JsonConvert.DeserializeObject<TResult>(content);
                    if (result==null) return new ErrorDataResult<TResult>(default(TResult), "Get data error. data is null");

                    return new SuccessDataResult<TResult>(result);
                }
                else
                {
                    return new ErrorDataResult<TResult>(default(TResult),"Get data error: Status code: "+response.StatusCode.ToString());
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<TResult>(default(TResult), exception.Message);
            }
        }

        public async Task<IDataResult<TResult>> GetAsync<TResult>(string requestUrl, StringContent requestContent)
        {
            try
            {
                var response=await _httpClient.PostAsync(requestUrl, requestContent);
                if (response==null) return new ErrorDataResult<TResult>(default(TResult), "Get data error. Response is null. ");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content==null) return new ErrorDataResult<TResult>(default(TResult), "Get data error. Content is null. ");

                    var result = JsonConvert.DeserializeObject<TResult>(content);
                    if (result==null) return new ErrorDataResult<TResult>(default(TResult), "Get data error. data is null");

                    return new SuccessDataResult<TResult>(result);
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return new ErrorDataResult<TResult>(default(TResult), "Get data error: Status code: "+
                        response.StatusCode.ToString()+" Error message: "+responseBody);
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<TResult>(default(TResult), exception.Message);
            }
        }
    }
}
