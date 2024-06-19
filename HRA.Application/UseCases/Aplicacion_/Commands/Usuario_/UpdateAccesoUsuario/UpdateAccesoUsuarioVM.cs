using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.UpdateAccesoUsuario
{
    public record class UpdateAccesoUsuarioVM : IRequest<Iresult>
    {
        public int I_USER_ID { get; set; }

        //[RequiredNull(ErrorMessage = "La fecha de inicio es requerida")]
        //public string D_START_DATE { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha de fin es requerida")]
        [RequiredNull(ErrorMessage = "La fecha de fin es requerida")]
        public string D_END_DATE { get; set; } = string.Empty;
    }
}
