using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Credito
{
    public class entity_listado_credito : BaseEntity
    {
        public int I_ID { get; set; }
        public string V_CUENTA { get; set; }
        public int I_ID_CUENTA { get; set; }
        public string V_TIPO_CUENTA { get; set; }
        public string V_TIPO_CREDITO { get; set; }
        public decimal I_MONTO_PRESTAMO { get; set; }
        public string V_FRECUENCIA_PAGO { get; set; }
        public int I_PLAZO_CANTIDAD { get; set; }
        public decimal I_TASA_INTERES { get; set; }
        public DateTime D_FECHA_DESEMBOLSO { get; set; }
        public decimal I_GASTO_FINANCIERO { get; set; }
        public decimal I_MONTO_REAL { get; set; }
        public DateTime D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
        
    }
}
