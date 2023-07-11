using BlazorWasmApp.Results.Abstract;

namespace BlazorWasmApp.HttpServices.Abstracts
{
    public interface IHttpRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"> TResult is response data type</typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        Task<IDataResult<TResult>> GetAsync<TResult>(string requestUrl);
        Task<IDataResult<TResult>> GetAsync<TResult>(string requestUrl, StringContent requestContent);
    }
}
