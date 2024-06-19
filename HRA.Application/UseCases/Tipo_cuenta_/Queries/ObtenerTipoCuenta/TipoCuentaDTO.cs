using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Tipo_cuenta_.Queries.ObtenerTipoCuenta
{
    public record class TipoCuentaDTO: IMapFrom<Tipo_cuenta>
    {
        public int I_PERSON_ID { get; set; }
        public string V_NUMBER_ACCOUNT { get; set; }
        public int I_ACCOUNT_TYPE_ID { get; set; }
        public string V_TYPE_ACCOUNT { get; set; }
        public void Mapping(Profile profile)
        {
        }
       
    }
}
