using HRA.Domain.Common;

namespace HRA.Domain.Entities.Application
{
    public class Rol : Auditoria_entity
    {
        public int I_ID_ROL { get; set; }
        public string V_ROL { get; set; } = string.Empty;
        public string V_DESCRIPCION { get; set; } = string.Empty;
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
