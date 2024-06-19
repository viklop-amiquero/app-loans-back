using HRA.Domain.Common;

namespace HRA.Domain.Entities.Operaciones
{
    public class Contacto_emergencia : Auditoria_entity
    {
        public int I_ID_CONTACTO_EM{ get; set; }
        public int I_ID_PERSONA { get; set; }
        public string? V_NOMBRE { get; set; }
        public string? V_PARENTESCO { get; set; }
        public string? V_CELULAR { get; set; }
        public string? V_TELEFONO { get; set; }
        public string B_ESTADO { get; set; }
    }
}
