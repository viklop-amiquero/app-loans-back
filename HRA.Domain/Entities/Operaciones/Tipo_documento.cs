using HRA.Domain.Common;

namespace HRA.Domain.Entities.Operaciones
{
    public class Tipo_documento : Auditoria_entity
    {
        public int I_ID_TIPO_DOC { get; set; }
        public string? V_ABREVIATURA { get; set; }
        public string V_NOMBRE_DOC { get; set; }
        public int? I_NRO_DIGITOS { get; set; }
        public string B_ESTADO { get; set; }
    }
}
