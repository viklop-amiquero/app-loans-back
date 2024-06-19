using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Interes_ahorro : Auditoria_entity
    {
        public int I_ID_INTERES_AHORRO { get; set; }
        public string V_NOMBRE { get; set; } 
        public decimal I_TASA_INTERES { get; set; }
        public string V_FRECUENCIA { get; set; }
        public string? V_DESCRIPCION { get; set; }
        public string B_ESTADO { get; set; } 
    }
}
