using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_usuario
{
    public record class ListaAplicacionDTO : IMapFrom<Usuario_Aplicacion>
    {
        public app Aplicaciones { get; set; }
        public string token { get; set; }
        public void Mapping(Profile profile)
        {
        }
    }

    public class app
    {
        public string id_app { get; set; }
        public string application { get; set; }
        public string acronym { get; set; }
        public string? url { get; set; }
        public string id_persona { get; set; }
        public string id_rol { get; set; }
        public string rol { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
    }

    public class token
    {
        public string id_app { get; set; }
        public string id_persona { get; set; }
        public string id_user { get; set; }
        public string dniUser { get; set; }
        public string namesUser { get; set; }     
        public string application { get; set; } 
        public string acronym { get; set; }
        public string id_rol { get; set; }
        public string Role { get; set; }

    }
}