using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Persona
{
    public class entity_Listado_persona : BaseEntity
    {
        public int I_ID { get; set; }
        public string V_DNI { get; set; }
        public string V_APELLIDO_PATERNO { get; set; }
        public string V_APELLIDO_MATERNO { get; set; }
        public string V_PRIMER_NOMBRE { get; set; }
        public string? V_DIRECCION { get; set; }
        public string? V_PROVINCIA { get; set; }
        public string? V_DISTRITO { get; set; }
        public string V_CELULAR { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string B_ESTADO { get; set; }
    }
}
