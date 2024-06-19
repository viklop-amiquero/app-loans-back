using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Cancelacion_mora : Auditoria_entity
    {
        public int I_ID_CANC_MORA { get; set; }
        public int I_ID_MORA { get; set; }
        public int I_ID_TIPO_CANC_MORA { get; set; }
        public decimal I_MONTO_CANC_MORA { get; set; }
        public decimal I_MONTO_INICIAL_MORA { get; set; }
        public decimal I_MONTO_FINAL_MORA { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
