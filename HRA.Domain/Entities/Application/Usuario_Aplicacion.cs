using HRA.Domain.Common;

namespace HRA.Domain.Entities.Application
{
    public class Usuario_Aplicacion : Auditoria_entity
    {
        public int I_ID_APLICACION_USUARIO { get; set; }
        public int I_ID_USUARIO { get; set; }
        public int I_ID_APLICACION_ROL_MENU { get; set; }
        public DateTime D_FECHA_INICIO { get; set; }
        public DateTime? D_FECHA_FIN { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
