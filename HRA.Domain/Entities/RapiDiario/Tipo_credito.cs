using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Tipo_credito : Auditoria_entity
    {
        public int I_ID_TIPO_CREDITO { get; set; }
        public string V_TIPO_CREDITO { get; set; }
        public string B_ESTADO { get; set; }
    }
}
