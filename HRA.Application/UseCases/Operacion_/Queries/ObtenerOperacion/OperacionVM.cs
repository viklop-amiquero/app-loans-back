using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Operacion_.Queries.ObtenerOperacion
{
    public record class OperacionVM : IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_OPERATION_ID { get; set; }
    }
}
