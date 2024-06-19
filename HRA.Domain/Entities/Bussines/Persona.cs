using HRA.Domain.Common;

namespace HRA.Domain.Entities.Bussines
{
    public class Persona : Auditoria_entity
    {
        public int I_ID_PERSONA { get; set; }
        public int I_ID_UBIGEO { get; set; }
        public int I_ID_SEXO { get; set; }
        public string V_PRIMER_NOMBRE { get; set; }
        public string? V_SEGUNDO_NOMBRE { get; set; }
        public string V_APELLIDO_PATERNO { get; set; }
        public string V_APELLIDO_MATERNO { get; set; }
        public int I_EDAD { get; set; }
        public DateTime D_FECHA_NACIMIENTO { get; set; }
        public string V_DIRECCION_DOMICILIO { get; set; }
        public string? V_DIRECCION_TRABAJO { get; set; }
        public string B_ESTADO { get; set; }
    }
}
