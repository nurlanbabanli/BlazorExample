using Microsoft.AspNetCore.Components;

namespace BlazorWasmApp.Shared
{
    public partial class NavMenu
    {
        private bool iconMenuActive = false;
        private bool collapseNavMenu = true;

        [Parameter]
        public EventCallback<bool> ShowIconMenu { get; set; }
        [Parameter]
        public bool HideIconText { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            NavigationManager.NavigateTo("/");
            collapseNavMenu = !collapseNavMenu;
        }

        private async Task ToggleIconMenu()
        {
            
            iconMenuActive = !iconMenuActive;
            await ShowIconMenu.InvokeAsync(iconMenuActive);          
        }
    }
}
