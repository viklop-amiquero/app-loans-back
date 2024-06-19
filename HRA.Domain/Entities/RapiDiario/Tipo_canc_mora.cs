using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Tipo_canc_mora : Auditoria_entity
    {
        public int I_ID_TIPO_CANC_MORA { get; set; }
        public string V_NOMBRE { get; set; } = string.Empty;
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
