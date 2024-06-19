using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Tramite_cuenta : Auditoria_entity
    {
        public int I_ID_TRAMITE_CUENTA { get; set; }
        public int I_ID_TRAMITE_DOC { get; set; }
        public int I_ID_CUENTA { get; set; }
        public string B_ESTADO { get; set; } = string.Empty;
    }
}
