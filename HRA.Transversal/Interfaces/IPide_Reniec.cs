using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA.Transversal.Interfaces
{
    public interface IPide_Reniec
    {
        string URL { get; set; }
        string Method { get; set; }
        string Usuario { get; set; }
        string RucUsuario { get; set; }
        string Password { get; set; }
    }
}
