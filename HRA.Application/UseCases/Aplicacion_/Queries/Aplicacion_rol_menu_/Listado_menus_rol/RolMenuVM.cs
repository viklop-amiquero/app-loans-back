using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Listado_menus_rol
{
    public record class RolMenuVM : IRequest<Iresult>
    {
        public int I_ROLE_ID { get; set; }
    }
}
