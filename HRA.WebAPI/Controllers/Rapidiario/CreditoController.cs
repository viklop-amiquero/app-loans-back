using HRA.Application.UseCases.Credito_.Commands.DeleteCredito;
using HRA.Application.UseCases.Credito_.Commands.NewCredito;
using HRA.Application.UseCases.Credito_.Queries.Lista_total_creditos;
using HRA.Application.UseCases.Credito_.Queries.Listado_creditos;
using HRA.Application.UseCases.Credito_.Queries.ObtenerCredito;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class CreditoController : BaseController
    {

        /// <summary>
        /// Paginado de creditos
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_credito([FromQuery] ListadoCreditosVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista todos los créditos
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_credito()
        {
            var r = await Mediator.Send(new CreditosVM { });
            return StatusCode(r.StatusCode, r);
        }

        // <summary>
        /// Lista un crédito
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_credito([FromQuery] CreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea un nuevo credito
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_credito([FromBody] NewCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///// <summary>
        ///// Modifica un credito
        ///// </summary>
        //[HttpPatch]
        ////[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Patch_update_credito([FromBody] UpdateCreditoVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        /// <summary>
        /// Dar de baja un credito
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_credito([FromQuery] DeleteCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

    }
}
