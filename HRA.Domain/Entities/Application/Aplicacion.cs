using HRA.Domain.Common;

namespace HRA.Domain.Entities.Application
{
    public class Aplicacion : Auditoria_entity
    {
        public int I_ID_APLICACION { get; set; }
        public string V_APLICACION { get; set; } = string.Empty;
        public string? V_ACRONIMO { get; set; }
        public string? V_DESCRIPCION { get; set; }
        public string? V_URL { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
