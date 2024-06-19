using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_SubCuota
{
    public class entity_listado_sub_cuota: BaseEntity
    {
        public int I_ID_SUB_CUOTA { get; set; }
        public int I_ID_CUOTA { get; set; }
        public decimal I_MONTO { get; set; }
        public decimal I_SALDO_CUOTA { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
