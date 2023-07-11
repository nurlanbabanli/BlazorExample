using BlazorWasmApp.HttpServices.Abstracts;
using BlazorWasmApp.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorWasmApp.Pages
{
    public partial class Login
    {
        private UserLoginDto userLoginDto=new UserLoginDto();

        [Inject]
        private IAuthenticationHttpService _authenticationHttpService { get; set; }
        [Inject]
        private IJSRuntime _jSRuntime { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private async Task HandleValidSubmit(EditContext editContext)
        {
            var loginResult = await _authenticationHttpService.LoginAsync(userLoginDto);
            if(loginResult == null)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastrError", "User login error");
            }
            else if (!loginResult.IsSuccess)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastrError", loginResult.Message);
            }
            else
            {
                _navigationManager.NavigateTo("/index");
            }
        }
    }
}
