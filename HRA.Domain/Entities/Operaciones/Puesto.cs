using HRA.Domain.Common;

namespace HRA.Domain.Entities.Operaciones
{
    public class Puesto : Auditoria_entity
    {
        public int I_ID_PUESTO { get; set; }
        public string V_NOMBRE { get; set; }
        public string V_DESCRIPCION { get; set; }
        public string B_ESTADO { get; set; }
    }
}
