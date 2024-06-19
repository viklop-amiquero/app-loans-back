using HRA.Application.UseCases.Cuota_.Commands.DeleteCuota;
using HRA.Application.UseCases.Cuota_.Commands.NewCuota;
using HRA.Application.UseCases.Cuota_.Queries.Lista_total_cuotas;
using HRA.Application.UseCases.Cuota_.Queries.Listado_cuotas;
using HRA.Application.UseCases.Cuota_.Queries.ObtenerCuotas;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class CuotaController : BaseController
    {
        /// <summary>
        /// Paginado de cuotas
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_cuota([FromQuery] ListadoCuotasVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Lista todos las cuotas
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_cuota()
        {
            var r = await Mediator.Send(new CuotasVM { });
            return StatusCode(r.StatusCode, r);
        }


        // <summary>
        /// Obtiene una cuota
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_cuota([FromQuery] CuotaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea una nueva cuota
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_cuota([FromBody] NewCuotaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///// <summary>
        ///// Modifica una cuota
        ///// </summary>
        //[HttpPatch]
        ////[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Patch_update_cuota([FromBody] UpdateCuotaVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        /// <summary>
        /// Dar de baja una cuota
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_cuota([FromQuery] DeleteCuotaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


    }
}
