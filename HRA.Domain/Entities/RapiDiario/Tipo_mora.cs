using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Tipo_mora : Auditoria_entity
    {
        public int I_ID_TIPO_MORA { get; set; }
        public string V_NOMBRE { get; set; }
        public decimal I_MONTO { get; set; }
        public string B_ESTADO { get; set; } 
    }
}
