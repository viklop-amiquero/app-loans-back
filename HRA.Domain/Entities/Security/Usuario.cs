using HRA.Domain.Common;

namespace HRA.Domain.Entities.Security
{
    public partial class Usuario : Auditoria_entity
    {
        public int I_ID_USUARIO { get; set; }
        public int I_ID_PERSONA { get; set; }
        public string V_USUARIO { get; set; } = string.Empty;
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
