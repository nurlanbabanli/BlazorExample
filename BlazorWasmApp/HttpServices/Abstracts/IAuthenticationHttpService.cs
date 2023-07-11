using BlazorWasmApp.Models.User;
using BlazorWasmApp.Results.Abstract;

namespace BlazorWasmApp.HttpServices.Abstracts
{
    internal interface IAuthenticationHttpService
    {
        Task<IDataResult<UserRegisterResponseDto>> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<IDataResult<UserLoginResponseDto>> LoginAsync(UserLoginDto userLoginDto);
    }
}
