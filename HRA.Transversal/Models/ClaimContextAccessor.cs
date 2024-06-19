using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HRA.Transversal.Models
{
    public class ClaimContextAccessor : IHttpContextAccessor
    {
        private HttpContext _httpContext;

        public HttpContext HttpContext
        {
            get => _httpContext ??= new DefaultHttpContext();
            set => _httpContext = value;
        }

        public string IDUser => GetClaimValue("IDUser");
        public string UserName => GetClaimValue("User_name");
        public string Name => GetClaimValue("Name");
        public string PaternalSurname => GetClaimValue("Paternal_surname");
        public string MaternalSurname => GetClaimValue("Maternal_surname");
        public string Document => GetClaimValue("Document");
        public IEnumerable<string> Roles => GetClaimValues("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        public bool ChangePassword => bool.TryParse(GetClaimValue("Change_password"), out bool result) && result;
        public string State => GetClaimValue("State");
        public string Jti => GetClaimValue("jti");
        public string Iat => GetClaimValue("iat");
        public string Nbf => GetClaimValue("nbf");
        public string Exp => GetClaimValue("exp");
        public string Iss => GetClaimValue("iss");
        public string Aud => GetClaimValue("aud");

        private string GetClaimValue(string claimType)
        {
            var identity = _httpContext?.User.Identity as ClaimsIdentity;
            return identity.Claims.FirstOrDefault(x => x.Type == claimType).Value;
        }

        private IEnumerable<string> GetClaimValues(string claimType)
        {
            var identity = _httpContext?.User.Identity as ClaimsIdentity;
            return identity?.FindAll(claimType)?.Select(c => c.Value) ?? Enumerable.Empty<string>();
        }
    }
}
