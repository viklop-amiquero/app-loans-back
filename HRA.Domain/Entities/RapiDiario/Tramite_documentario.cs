using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public partial class Tramite_documentario : Auditoria_entity
    {
        public int I_ID_TRAMITE_DOC { get; set; }
        public string V_NOMBRE { get; set; }
        public decimal I_TARIFA { get; set; }
        public string? V_DESCRIPCION { get; set; }
        public string B_ESTADO { get; set; } 
    }
}
