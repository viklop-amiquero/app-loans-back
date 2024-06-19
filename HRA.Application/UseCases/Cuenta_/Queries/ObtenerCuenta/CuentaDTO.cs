using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Cuenta_.Queries.ObtenerCuenta
{
    public record class CuentaDTO: IMapFrom<Cuenta>
    {
        public int I_PERSON_ID { get; set; }
        public string V_NRO_DOCUMENT { get; set; }
        public string V_FIRST_NAME { get; set; }
        public string? V_SECOND_NAME { get; set; }
        public string V_PATERNAL_LAST_NAME { get; set; }
        public string V_MOTHER_LAST_NAME { get; set; }
        
        public List<cuenta_cliente> Cuentas_cliente { get; set; }

        
        public void Mapping(Profile profile)
        {
        }
       
    }
    public class cuenta_cliente
    {
        public int I_ACCOUNT_ID { get; set; }
        public int I_ACCOUNT_TYPE_ID { get; set; }
        public string V_ACCOUNT_NUMBER { get; set; }
        public decimal I_BALANCE { get; set; }
        public string V_USER_CREATE { get; set; }
        public string? V_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime? D_MODIF_DATE { get; set; }
        public string B_STATE { get; set; }
    }
}
