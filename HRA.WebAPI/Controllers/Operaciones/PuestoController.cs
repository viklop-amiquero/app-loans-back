using HRA.Application.UseCases.Puesto_.Commands.ActivatePuesto;
using HRA.Application.UseCases.Puesto_.Commands.DeletePuesto;
using HRA.Application.UseCases.Puesto_.Commands.NewPuesto;
using HRA.Application.UseCases.Puesto_.Commands.UpdatePuesto;
using HRA.Application.UseCases.Puesto_.Queries.ListaTotalPuestos;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Operaciones
{
    public class PuestoController : BaseController
    {

        /// <summary>
        /// Lista todos los puestos
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_puesto()
        {
            var r = await Mediator.Send(new PuestosVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un puesto
        /// </summary>
        //[HttpGet]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Get_puesto([FromQuery] PuestoVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        /// <summary>
        /// Crea un nuevo puesto
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_puesto([FromBody] NewPuestoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Modifica un puesto
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_puesto([FromBody] UpdatePuestoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Activa un puesto
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_puesto([FromQuery] ActivatePuestoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Dar de baja un puesto
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_puesto([FromQuery] DeletePuestoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
