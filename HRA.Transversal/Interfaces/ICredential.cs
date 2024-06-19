using HRA.Transversal.Models;

namespace HRA.Transversal.Interfaces
{
    public interface ICredential
    {
        List<Correo> Gmail { get; set; }
        List<Correo> Outlook { get; set; }
        string ClientG { get; set; }
        string ClientO { get; set; }
        string Port { get; set; }
        string Cco { get; set; }
    }
}
