using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Credito_.Queries.ObtenerCredito
{
    public record class CreditoVM : IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_CREDIT_ID { get; set; }
    }
}
