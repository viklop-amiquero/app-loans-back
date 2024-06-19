using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure;

namespace HRA.Application.UseCases.Login_.Queries.ExpiredAccess
{
    public record ExpiredAccessDTO : IMapFrom<entity_Expired_access>
    {
        public int ID_User { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_Expired_access, ExpiredAccessDTO>()
            .ForMember(dto => dto.ID_User, et => et.MapFrom(a => a.ID_USER));
        }
    }
}
