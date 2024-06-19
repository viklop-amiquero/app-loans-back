using HRA.Domain.Common;

namespace HRA.Domain.Entities.Registro
{
    public class Documento_persona : Auditoria_entity
    {
        public int I_ID_DOCUMENTO_PERS { get; set; }
        public int I_ID_TIPO_DOC { get; set; }
        public int I_ID_PERSONA { get; set; }
        public string V_NRO_DOCUMENTO { get; set; }
        public string B_ESTADO { get; set; }
    }
}
