using HRA.Application.UseCases.Sub_cuota_.Commands.DeleteSubCuota;
using HRA.Application.UseCases.Sub_cuota_.Queries.Listado_cuota;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class SubCuotaController : BaseController
    {
        /// <summary>
        /// Paginado de sub cuotas
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_sub_cuota([FromQuery] ListadoSubCuotaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Dar de baja una sub cuota
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_sub_cuota([FromQuery] DeleteSubCuotaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

    }
}
