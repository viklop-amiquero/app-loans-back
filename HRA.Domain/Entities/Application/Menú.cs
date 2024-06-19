using HRA.Domain.Common;

namespace HRA.Domain.Entities.Application
{
    public class Menú : Auditoria_entity
    {
        public int I_ID_MENU { get; set; }
        public int I_ID_APLICACION { get; set; }
        public string V_MENU { get; set; } = string.Empty;
        public string? V_DESCRIPCION { get; set; }
        public string? V_ICONO { get; set; }
        public string? V_RUTA { get; set; }
        public string? V_URL { get; set; }
        public Guid V_NIVEL_PARENTESCO { get; set; }
        public string? V_PARENTESCO { get; set; }
        public int I_NIVEL { get; set; }
        public int I_ORDEN { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
