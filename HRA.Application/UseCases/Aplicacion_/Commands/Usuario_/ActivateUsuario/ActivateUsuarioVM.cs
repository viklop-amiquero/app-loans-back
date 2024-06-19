using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.ActivateUsuario
{
    public record class ActivateUsuarioVM : IRequest<Iresult>
    {
        public int I_ID_USER { get; set; }
    }
}
