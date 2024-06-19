using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Listado_usuarios
{
    public record class ListadoUsuariosVM : IRequest<Iresult>
    {
        public int? I_PAGE_NUMBER { get; set; }
        public int? I_PAGE_SIZE { get; set; }
        public string? V_FILTER_TYPE { get; set; }
        public string? V_FILTER_VALUE { get; set; }
        public int? I_SORT_BY_FIELD { get; set; }
        public string? V_SORT_ORDER { get; set; }
    }
}
