using HRA.Domain.Common;

namespace HRA.Domain.Entities.Registro
{
    public class Puesto_persona : Auditoria_entity
    {
        public int I_ID_PUESTO_PERS { get; set; }
        public int I_ID_PERSONA { get; set; }
        public int I_ID_PUESTO { get; set; }
        public string B_ESTADO { get; set; }

    }
}
