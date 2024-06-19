using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Sub_cuota : Auditoria_entity
    {
        public int I_ID_SUB_CUOTA { get; set; }
        public int I_ID_CUOTA { get; set; }
        public decimal I_MONTO { get; set; }
        public decimal I_SALDO_CUOTA { get; set; }
        public string B_ESTADO { get; set; }
    }
}
