using HRA.Transversal.Interfaces;

namespace HRA.Transversal.Models
{
    public class Sis : ISis
    {
        public string URL { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
