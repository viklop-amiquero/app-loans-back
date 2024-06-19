using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Usuario_Aplicacion
{
    public class entity_Listado_usuario_app : BaseEntity
    {
        public int I_ID { get; set; }
        public string? V_USUARIO { get; set; }
        public string? V_NOMBRE_COMPLETO{ get; set; }
        public string? V_ROL { get; set; }
        public DateTime? D_FECHA_INICIO { get; set; }
        public DateTime? D_FECHA_FIN { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
