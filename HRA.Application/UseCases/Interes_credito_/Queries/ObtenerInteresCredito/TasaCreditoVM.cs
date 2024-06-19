using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Interes_credito_.Queries.ObtenerInteresCredito
{
    public record class TasaCreditoVM : IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_INTEREST_ID { get; set; }
    }
}
