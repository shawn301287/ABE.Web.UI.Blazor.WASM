using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABE.Web.UI.Blazor.WASM.Models.Authentication
{
    public class AuthResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
