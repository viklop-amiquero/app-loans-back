using HRA.Domain.Common;

namespace HRA.Domain.Entities.Operaciones
{
    public class Ubigeo : Auditoria_entity
    {

        public int I_ID_UBIGEO { get; set; }
        public string V_CODIGO_DEPARTAMENTO { get; set; }
        public string? V_DEPARTAMENTO { get; set; }
        public string? V_CODIGO_PROVINCIA { get; set; }
        public string? V_PROVINCIA { get; set; }
        public string? V_CODIGO_DISTRITO { get; set; }
        public string? V_DISTRITO { get; set; }
        public string? V_CAPITAL { get; set; }
        public string? V_ALTITUDE { get; set; }
        public string? V_LATITUDE { get; set; }
        public string? V_LONGITUDE { get; set; }
        public string B_ESTADO { get; set; }

    }
}
