using HRA.Domain.Common;

namespace HRA.Domain.Entities.Security
{
    public class Clave : Auditoria_entity
    {
        public int I_ID_CLAVE { get; set; }
        public int I_ID_USUARIO { get; set; }
        public byte[] V_CLAVE_HASH { get; set; }
        public string? B_RESTABLECER { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
