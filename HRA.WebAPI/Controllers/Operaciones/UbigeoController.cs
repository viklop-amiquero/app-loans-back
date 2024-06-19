using HRA.Application.UseCases.Ubigeo_.Queries.ListaTotalDepartamento;
using HRA.Application.UseCases.Ubigeo_.Queries.ListaTotalDistrito;
using HRA.Application.UseCases.Ubigeo_.Queries.ListaTotalProvincia;
using HRA.Application.UseCases.Ubigeo_.Queries.ObtenerUbigeo;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Operaciones
{
    public class UbigeoController : BaseController
    {
        /// <summary>
        /// Lista todo el deparatemento
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_total_departamento()
        {
            var r = await Mediator.Send(new DepartamentoVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista toda la provincia
        /// </summary>
        
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_total_provincia([FromQuery] ProvinciaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Lista todo el distrito
        /// </summary>

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_total_distrito([FromQuery] DistritoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Obtiene un ubigeo por id
        /// </summary>

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_ubigeo([FromQuery] UbigeoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
