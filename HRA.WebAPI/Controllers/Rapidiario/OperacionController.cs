using HRA.Application.UseCases.Operacion_.Commands.DeleteOperacion;
using HRA.Application.UseCases.Operacion_.Commands.NewOperacion;
using HRA.Application.UseCases.Operacion_.Queries.Lista_total_operaciones;
using HRA.Application.UseCases.Operacion_.Queries.Listado_operaciones;
using HRA.Application.UseCases.Operacion_.Queries.ObtenerOperacion;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class OperacionController : BaseController
    {
        /// <summary>
        /// Paginado de operaciones
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_operacion([FromQuery] ListadoOperacionesVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista todas las operaciones
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_operacion()
        {
            var r = await Mediator.Send(new OperacionesVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Obtiene una operacion
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_operacion([FromQuery] OperacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea una nueva operacion
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_operacion([FromBody] NewOperacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Dar de baja una operacion
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_operacion([FromQuery] DeleteOperacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

    }
}
