using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Operaciones;

namespace HRA.Application.UseCases.Sexo_.Queries.Lista_total_sexos
{
    public record class SexoDTO : IMapFrom<Sexo>
    {
        public int I_SEX_ID { get; set; }
        public string V_NAME { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sexo, SexoDTO>()
                .ForMember(dto => dto.I_SEX_ID, et => et.MapFrom(a => a.I_ID_SEXO))
                .ForMember(dto => dto.V_NAME, et => et.MapFrom(a => a.V_NOMBRE))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
