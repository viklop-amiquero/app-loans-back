using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Operacion : Auditoria_entity
    {
        public int I_ID_OPERACION { get; set; }
        public int I_ID_CUENTA { get; set; }
        public int? I_ID_CUOTA { get; set; }
        public int I_ID_TIPO_OPERACION { get; set; }
        public string V_NUMERO_OPERACION { get; set; }
        public decimal I_MONTO { get; set; }
        public string B_ESTADO { get; set; }
    }
}
