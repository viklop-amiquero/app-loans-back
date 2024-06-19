using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.DeleteUsuarioAplicacion
{
    public record class DeleteUsuarioAplicacionVM : IRequest<Iresult>
    {
        public int I_USER_APP_ID { get; set; }
    }
}
