using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWasmApp.Shared
{
    public partial class MainLayout
    {
        private bool _iconMenuActive;
        private string iconMenuCssClass = "width:320px";
        private bool hideIconText = false;
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        protected async Task ToggleIconMenu(bool iconMenuActive)
        {
            _iconMenuActive = iconMenuActive;

            _=Task.Run(async () =>
           {
               if (iconMenuActive)
               {
                   for (int i = 320; i > 80; i=i-2)
                   {
                       await Task.Delay(1);
                       iconMenuCssClass="width:"+i+"px";
                       if (i<100) hideIconText=true;
                       await InvokeAsync(() => StateHasChanged());
                   }
               }
               else
               {
                   hideIconText=false;
                   for (int i = 80; i < 320; i=i+2)
                   {
                       await Task.Delay(1);
                       iconMenuCssClass="width:"+i+"px";
                       await InvokeAsync(() => StateHasChanged());


                   }
                   await InvokeAsync(() => StateHasChanged());
               }
           });


        }

        protected override async Task OnInitializedAsync()
        {

        }

        //int currentScreenWithd = 0;
        //bool inMediaScreen
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //var screenWidth = await JSRuntime.InvokeAsync<int>("getScreenWidth");

            //if (screenWidth<640 && currentScreenWithd<640)
            //{
            //    currentScreenWithd=640;
            //    iconMenuCssClass="width:"+currentScreenWithd+"px";
            //    await InvokeAsync(() => StateHasChanged());
            //}
        }
    }
    public class BrowserDimension
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
