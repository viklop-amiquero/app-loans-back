using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.UpdateUsuario
{
    public record class UpdateUsuarioVM : IRequest<Iresult>
    {
        // No seria necesario porque el mismo usuario logueado deberia cambiar su contraseña
        //public int I_USER_ID { get; set; }

        public string V_PASS { get; set; } = string.Empty;
    }
}
