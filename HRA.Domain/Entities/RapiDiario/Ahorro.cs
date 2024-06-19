using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Ahorro : Auditoria_entity
    {
        public int I_ID_AHORRO { get; set; }
        public int I_ID_CUENTA { get; set; }
        public int I_ID_INTERES_AHORRO { get; set; }
        public string? V_TIPO_AHORRO { get; set; }
        public decimal? I_MONTO_AHORRO { get; set; }
        public int? I_MESES { get; set; }
        public decimal? I_INSCRIPCION { get; set; }
        public decimal? I_MONTO_ULTIMA_TRANSACCION { get; set; }
        public string B_ESTADO { get; set; }

    }
}
