using ABE.Web.UI.Blazor.WASM.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABE.Web.UI.Blazor.WASM.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> RegisterAsync(RegisterModel model);
        Task<AuthResponse> LoginAsync(LoginModel model);
        Task<bool> LogoutAsync();
    }
}
