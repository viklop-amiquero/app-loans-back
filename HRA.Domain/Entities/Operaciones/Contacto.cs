using HRA.Domain.Common;

namespace HRA.Domain.Entities.Operaciones
{
    public class Contacto : Auditoria_entity
    {
        public int I_ID_CONTACTO { get; set; }
        public int I_ID_CONTACTO_EM { get; set; }
        public string? V_TELEFONO { get; set; }
        public string V_CELULAR { get; set; }
        public string? V_CORREO { get; set; }
        public string B_ESTADO { get; set; }
    }
}
