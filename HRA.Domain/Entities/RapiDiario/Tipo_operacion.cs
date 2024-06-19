using HRA.Domain.Common;

namespace HRA.Domain.Entities.RapiDiario
{
    public class Tipo_operacion : Auditoria_entity
    {
        public int I_ID_TIPO_OPERACION { get; set; }
        public string V_IDENTIFICADOR { get; set; }
        public string V_TIPO_OPERACION { get; set; }
        public string? V_DESCRIPCION { get; set; }
        public string B_ESTADO { get; set; }

    }
}
