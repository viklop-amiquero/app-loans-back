using HRA.Domain.Common;

namespace HRA.Domain.Entities.Operaciones
{
    public class Sexo : Auditoria_entity
    {
        public int I_ID_SEXO { get; set; }
        public string V_NOMBRE { get; set; }
        public string B_ESTADO { get; set; }
    }
}
