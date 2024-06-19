using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Credito : Auditoria_entity
    {
        public int I_ID_CREDITO { get; set; }
        public int I_ID_CUENTA { get; set; }
        public int I_ID_TIPO_CREDITO { get; set; }
        public int I_ID_FREC_PAGO { get; set; }
        public int I_ID_INTERES_CREDITO { get; set; }
        public decimal I_MONTO_PRESTAMO { get; set; }
        public int I_PLAZO_CANTIDAD { get; set; }
        public DateTime D_FECHA_DESEMBOLSO { get; set; }
        public decimal I_GASTO_FINANCIERO { get; set; }
        public decimal I_MONTO_REAL { get; set; }
        public string B_ESTADO { get; set; }
    }
}
