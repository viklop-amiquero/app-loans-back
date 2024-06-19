using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_AplicacionRolMenu
{
    public class entity_Listado_aplicacion_rol_menu : BaseEntity
    {
        public int I_ID { get; set; }
        public string? V_ROL { get; set; }
        //public string? V_APP { get; set; }
        public string? V_MENU { get; set; }
        public string? V_PERMISO { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
