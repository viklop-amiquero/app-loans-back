using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Cuenta;

namespace HRA.Application.UseCases.Cuenta_.Queries.Listado_cuentas
{
    public record class ListadoCuentasDTO : IMapFrom<entity_listado_cuenta>
    {
        public int I_ACCOUNT_ID { get; set; }
        public int I_PERSON_ID { get; set; }
        public int I_TYPE_ACCOUNT_ID { get; set; }
        public string V_NUMBER_DOC { get; set; }
        public string V_NUMBER_ACCOUNT { get; set; }
        public string V_FIRST_NAME { get; set; }
        public string? V_SECOND_NAME { get; set; }
        public string V_PATERNAL_LAST_NAME { get; set; }
        public string V_MOTHER_LAST_NAME { get; set; }
        public decimal I_SALDO { get; set; }
        public string B_STATE { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_listado_cuenta, ListadoCuentasDTO>()
                .ForMember(dto => dto.I_ACCOUNT_ID, et => et.MapFrom(a => a.I_ID))
                .ForMember(dto => dto.I_PERSON_ID, et => et.MapFrom(a => a.I_ID_PERSONA))
                .ForMember(dto => dto.I_TYPE_ACCOUNT_ID, et => et.MapFrom(a => a.I_ID_TIPO_CUENTA))
                .ForMember(dto => dto.V_NUMBER_DOC, et => et.MapFrom(a => a.V_NRO_DOC))
                .ForMember(dto => dto.V_NUMBER_ACCOUNT, et => et.MapFrom(a => a.V_NUMERO_CUENTA))
                .ForMember(dto => dto.V_FIRST_NAME, et => et.MapFrom(a => a.V_PRIMER_NOMBRE))
                .ForMember(dto => dto.V_SECOND_NAME, et => et.MapFrom(a => a.V_SEGUNDO_NOMBRE))
                .ForMember(dto => dto.V_PATERNAL_LAST_NAME, et => et.MapFrom(a => a.V_APELLIDO_PATERNO))
                .ForMember(dto => dto.V_MOTHER_LAST_NAME, et => et.MapFrom(a => a.V_APELLIDO_MATERNO))
                .ForMember(dto => dto.I_SALDO, et => et.MapFrom(a => a.I_SALDO))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}