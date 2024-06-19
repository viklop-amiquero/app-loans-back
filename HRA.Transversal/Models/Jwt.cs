using HRA.Transversal.Interfaces;

namespace HRA.Transversal.Models
{
    public class Jwt : IAuthenticationJWT
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
    }
}
