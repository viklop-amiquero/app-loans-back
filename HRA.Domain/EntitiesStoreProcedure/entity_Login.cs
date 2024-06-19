using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure
{
    public class entity_Login : BaseEntity
    {
        public int I_ID_USUARIO { get; set; }
        public int I_ID_PERSONA { get; set; }
        public string? V_USUARIO { get; set; }
        public string? V_PRIMER_NOMBRE { get; set; }
        public string? V_APELLIDO_PATERNO { get; set; }
        public string? V_APELLIDO_MATERNO { get; set; }
        public int I_ID_ROL { get; set; }
        public string? V_ROL { get; set; }
        public bool B_CAMBIO_CONTRASENIA { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
