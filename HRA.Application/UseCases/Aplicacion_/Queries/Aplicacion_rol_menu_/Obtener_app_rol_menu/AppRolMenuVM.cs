using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Obtener_app_rol_menu
{
    public record class AppRolMenuVM : IRequest<Iresult>
    {
        public int I_APP_ROL_MENU_ID { get; set; }
    }
}
