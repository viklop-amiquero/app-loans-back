using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Puesto_.Commands.DeletePuesto
{
    public record class DeletePuestoVM : IRequest<Iresult>
    {
        /// <summary>
        /// delete puesto
        /// </summary>
        public int I_POSITION_ID { get; set; }
       
    }
}
