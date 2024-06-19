using HRA.Application.UseCases.Cuenta_.Commands.ActivateCuenta;
using HRA.Application.UseCases.Cuenta_.Commands.DeleteCuenta;
using HRA.Application.UseCases.Cuenta_.Commands.NewCuenta;
using HRA.Application.UseCases.Cuenta_.Queries.Lista_total_cuentas;
using HRA.Application.UseCases.Cuenta_.Queries.Listado_cuentas;
using HRA.Application.UseCases.Cuenta_.Queries.ObtenerCuenta;
using HRA.Application.UseCases.Cuenta_.Queries.PaginadoCuentas;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class CuentaController : BaseController
    {

        /// <summary>
        /// Paginado de cuentas
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_cuenta([FromQuery] ListadoCuentasVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Paginado cuentas de cada cliente
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_paginado_cuenta([FromQuery] PaginadoCuentasVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista todas las cuentas
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_cuenta()
        {
            var r = await Mediator.Send(new CuentasVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista una cuenta
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_cuenta([FromQuery] CuentaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea una nueva cuenta
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_cuenta([FromBody] NewCuentaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica una cuenta
        /// </summary>
        //[HttpPatch]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Patch_update_cuenta([FromBody] UpdateCuentaVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        /// <summary>
        /// Dar de baja una cuenta
        /// </summary>

        /// <summary>
        /// Activa una cuenta
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_cuenta([FromQuery] ActivateCuentaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_cuenta([FromQuery] DeleteCuentaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

    }
}
