using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Cuota
{
    public class entity_listado_cuota: BaseEntity
    {
        public int I_ID { get; set; }
        public int I_ID_CUENTA { get; set; }
        public int I_ID_CREDITO { get; set; }
        public string V_NUMERO_CUOTA { get; set; }
        public decimal I_MONTO_CUOTA { get; set; }
        public decimal I_CAPITAL { get; set; }
        public decimal I_SALDO_INICIAL { get; set; }
        public decimal I_INTERES { get; set; }
        public decimal I_SALDO_FINAL { get; set; }
        public DateTime? D_FECHA_PAGO { get; set; }
        public decimal? I_MONTO_TOTAL { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }

    }
}
