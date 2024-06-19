using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuenta_.Queries.ObtenerCuenta
{
    public record class CuentaVM: IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_PERSON_ID { get; set; }
    }
}
