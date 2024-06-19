using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Tipo_credito_.Queries.ListaTipoCredito
{
    
    public record class TiposCreditosVM : IRequest<Iresult>
    {
    }
}
