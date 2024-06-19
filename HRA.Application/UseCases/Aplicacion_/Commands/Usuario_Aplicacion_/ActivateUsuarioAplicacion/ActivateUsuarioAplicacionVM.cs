using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.ActivateUsuarioAplicacion
{
    public record class ActivateUsuarioAplicacionVM : IRequest<Iresult>
    {
        public int I_USER_APP_ID { get; set; }
    }
}
