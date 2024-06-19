using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Obtener_pers_usuario
{
    public record class UsuarioPersVM : IRequest<Iresult>
    {
        public string V_USER_NAME { get; set; } = string.Empty;
    }
}
