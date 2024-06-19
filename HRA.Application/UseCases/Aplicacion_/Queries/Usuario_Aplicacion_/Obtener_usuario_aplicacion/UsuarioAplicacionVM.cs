using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Obtener_usuario_aplicacion
{
    public record class UsuarioAplicacionVM : IRequest<Iresult>
    {
        public int I_USER_ID { get; set; }
    }
}
