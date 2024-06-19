using HRA.Transversal.Interfaces;

namespace HRA.Transversal.Models
{
    public class Minsa_Reniec : IMinsa_Reniec
    {
        public string URL { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }
}
