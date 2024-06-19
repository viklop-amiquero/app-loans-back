
using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Aplicacion
{
    public class entity_Listado_aplicacion : BaseEntity
    {
        public int I_ID { get; set; }
        public string? V_APLICACION { get; set; }
        public string? V_ACRONIMO { get; set; }
        public string? V_MENU { get; set; }
        public string? V_DESCRIPCION { get; set; }
        public string? V_URL { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
