using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Cuenta
{
    public class entity_paginado_cuenta : BaseEntity
    {
        public string I_ID { get; set; }
        public int I_ID_PERSONA { get; set; }
        public string I_ID_TIPO_CUENTA { get; set; }
        public string V_NRO_DOC { get; set; }
        public string V_NUMERO_CUENTA { get; set; }
        public string V_PRIMER_NOMBRE { get; set; }
        public string? V_SEGUNDO_NOMBRE { get; set; }
        public string V_APELLIDO_PATERNO { get; set; }
        public string V_APELLIDO_MATERNO { get; set; }
        public string I_SALDO { get; set; }
        public string B_ESTADO { get; set; }

    }
}