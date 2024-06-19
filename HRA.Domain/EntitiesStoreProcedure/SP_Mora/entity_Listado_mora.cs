using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Mora
{
    public class entity_Listado_mora : BaseEntity
    {
        public int I_ID { get; set; }
        public int I_ID_CUOTA { get; set; }
        public double I_MONTO_MORA { get; set; }
        public int I_NUMERO_DIA { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
