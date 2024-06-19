using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_rol
{
    public record class ListaMenuRolDTO : IMapFrom<Aplicacion_Rol_Menu>
    {
        public void Mapping(Profile profile)
        {
            //profile.CreateMap<User_menu, ListaMenuDTO>()
            //.ForMember(dto => dto.Applications, et => et.MapFrom(a => a.I_ID_MENU))
            //.ForMember(dto => dto.Applications, et => et.MapFrom(a => a.I_ID_USER))
            //.ForMember(dto => dto.Order, et => et.MapFrom(a => a.I_ORDEN));
        }
    }

    public class Menu_
    {
        public int id_menu { get; set; }
        public string menu { get; set; }
        public string icon { get; set; }
        public string ruta { get; set; }
        public string url { get; set; }
        public string id_parentesco { get; set; }
        public string parentesco { get; set; }
        public int nivel { get; set; }
        public int orden { get; set; }
        public Permiso_DTO? permiso { get; set; }
    }

    public class Permiso_DTO
    {
        public int IDPermission { get; set; }
        public int Create { get; set; }
        public int Read { get; set; }
        public int Update { get; set; }
        public int Delete { get; set; }
        public string Description { get; set; }
    }
}
