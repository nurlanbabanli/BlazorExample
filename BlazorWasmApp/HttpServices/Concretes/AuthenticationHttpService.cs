using BlazorWasmApp.HttpServices.Abstracts;
using BlazorWasmApp.Models.User;
using BlazorWasmApp.Results.Abstract;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWasmApp.HttpServices.Concretes
{
    internal class AuthenticationHttpService : IAuthenticationHttpService
    {
         private readonly IHttpRepository _httpRepository;

        public AuthenticationHttpService(IHttpRepository httpRepository)
        {
            _httpRepository=httpRepository;
        }

        public async Task<IDataResult<UserLoginResponseDto>> LoginAsync(UserLoginDto userLoginDto)
        {
            var jsonLoginModel=JsonConvert.SerializeObject(userLoginDto);
            var registerContent = new StringContent(jsonLoginModel, Encoding.UTF8, "application/json");
            var reguestUrl = "/api/auth/loginUser";

            return await _httpRepository.GetAsync<UserLoginResponseDto>(reguestUrl, registerContent);
        }

        public async Task<IDataResult<UserRegisterResponseDto>> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var jsonRegisterModel=JsonConvert.SerializeObject(userRegisterDto);
            var registerContent = new StringContent(jsonRegisterModel, Encoding.UTF8, "application/json");
            var requestUtl = "/api/auth/registerUser";

            return await _httpRepository.GetAsync<UserRegisterResponseDto>(requestUtl, registerContent);
        }
    }
}
