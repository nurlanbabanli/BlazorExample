using BlazorWasmApp.HttpServices.Abstracts;
using BlazorWasmApp.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorWasmApp.Pages
{
    public partial class Registration
    {
        private UserRegisterDto userRegisterDto = new UserRegisterDto();
        [Inject]
        private IAuthenticationHttpService _authenticationHttpService { get; set; }
        [Inject]
        private IJSRuntime _jsRuntime { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private async Task HandleValidSubmit(EditContext editContext)
        {
            var registerResult = await _authenticationHttpService.RegisterAsync(userRegisterDto);
            if (registerResult==null)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", "User register error");
            }
            else if (!registerResult.IsSuccess)
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrError", registerResult.Message);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastrSuccess", "User added");
                _navigationManager.NavigateTo("/login");
            }
        }

        private async Task RegisterCancelHandler()
        {

        }
    }
}
