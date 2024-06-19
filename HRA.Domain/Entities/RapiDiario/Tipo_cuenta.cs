using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Tipo_cuenta : Auditoria_entity
    {
        public int I_ID_TIPO_CUENTA { get; set; }
        public string V_TIPO_CUENTA { get; set; }
        public string B_ESTADO { get; set; }
    }
}
