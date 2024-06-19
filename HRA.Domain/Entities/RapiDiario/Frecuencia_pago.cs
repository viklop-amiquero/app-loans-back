using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public partial class Frecuencia_pago : Auditoria_entity
    {
        public int I_ID_FREC_PAGO { get; set; }
        public string V_NOMBRE { get; set; } = null!;
        public string? V_DESCRIPCION { get; set; }
        public string B_ESTADO { get; set; } = null!;
  
    }
}
