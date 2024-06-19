using HRA.Transversal.Interfaces;

namespace HRA.Transversal.Models
{
    public class Pide_Reniec : IPide_Reniec
    {
        public string URL { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string RucUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
