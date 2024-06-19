using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Cuenta : Auditoria_entity
    {
        public int I_ID_CUENTA { get; set; }
        public int I_ID_PERSONA { get; set; }
        public int I_ID_TIPO_CUENTA { get; set; }
        public string V_NUMERO_CUENTA { get; set; }
        public decimal I_SALDO { get; set; }
        public string B_ESTADO { get; set; }
    }
}
