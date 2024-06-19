using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Rol
{
    public class entity_Listado_rol : BaseEntity
    {
        public int I_ID { get; set; }
        public string? V_ROL { get; set; }
        public string? V_DESCRIPCION { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
