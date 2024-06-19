using HRA.Domain.Common;

namespace HRA.Domain.Entities.Application
{
    public class Permiso : Auditoria_entity
        {
        public int I_ID_PERMISO { get; set; }
        public int I_C { get; set; }
        public int I_R { get; set; }
        public int I_U { get; set; }
        public int I_D { get; set; }
        public string? V_DESCRIPCION { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
