using HRA.Domain.Common;

namespace HRA.Domain.Entities.Application
{
    public class Aplicacion_Rol_Menu : Auditoria_entity
    {
        public int I_ID_APLICACION_ROL_MENU { get; set; }
        public int I_ID_MENU { get; set; }
        public int I_ID_ROL { get; set; }
        public int I_ID_PERMISO { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
