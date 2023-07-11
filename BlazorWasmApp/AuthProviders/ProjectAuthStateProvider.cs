using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorWasmApp.AuthProviders
{
    internal class ProjectAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //await Task.Delay(2000);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Nurlan Babanli"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var anonymous = new ClaimsIdentity(claims, "testAuthType");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}
