using HRA.Transversal.Interfaces;

namespace HRA.Transversal.Models
{
    public class Credentials : ICredential
    {
        public List<Correo> Gmail { get; set; } = new List<Correo>();
        public List<Correo> Outlook { get; set; } = new List<Correo>();
        public string ClientG { get; set; } = string.Empty;
        public string ClientO { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string Cco { get; set; } = string.Empty;
    }
    public class Correo 
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
