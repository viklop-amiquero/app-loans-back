using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.DeleteUsuario
{
    public record class DeleteUsuarioVM : IRequest<Iresult>
    {
        public int I_ID_USER { get; set; }
    }
}
