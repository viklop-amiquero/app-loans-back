using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.DeleteAplicacionRolMenu
{
    public record class DeleteAplicacionRolMenuVM : IRequest<Iresult>
    {
        public int I_APPLICATION_ROLE_MENU_ID { get; set; }
    }
}
