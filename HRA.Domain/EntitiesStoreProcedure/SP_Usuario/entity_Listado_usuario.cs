using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Usuario
{
    public class entity_Listado_usuario : BaseEntity
    {
        public int I_ID { get; set; }
        public string? V_USUARIO { get; set; }
        public string? V_PRIMER_NOMBRE { get; set; }
        public string? V_APELLIDO_PATERNO { get; set; }
        public string? V_APELLIDO_MATERNO { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
