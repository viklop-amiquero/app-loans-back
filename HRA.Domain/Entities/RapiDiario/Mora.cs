using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Mora : Auditoria_entity
    {
        public int I_ID_MORA { get; set; }
        public int I_ID_CUOTA { get; set; }
        public int I_ID_TIPO_MORA { get; set; }
        public decimal I_MONTO_MORA { get; set; }
        public int I_NUMERO_DIA { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
