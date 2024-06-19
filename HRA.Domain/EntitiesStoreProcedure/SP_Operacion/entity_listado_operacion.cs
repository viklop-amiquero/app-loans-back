using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Operacion
{
    public class entity_listado_operacion : BaseEntity
    {
        public int I_ID_OPERACION { get; set; }
        public int I_ID_CUENTA { get; set; }
        public int? I_ID_CUOTA { get; set; }
        public string V_TIPO_OPERACION { get; set; }
        public string V_NUMERO_OPERACION { get; set; }
        public decimal I_MONTO { get; set; }
        public string V_NUMERO_CUENTA { get; set; }
        public string? V_REVERSION { get; set; }
        public string? B_ESTADO { get; set; }
        public DateTime D_FECHA_CREACION { get; set; }
    }
}
