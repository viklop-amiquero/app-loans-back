using HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Lista_total_roles;
using HRA.Application.UseCases.Persona_.Commands.ActivatePersona;
using HRA.Application.UseCases.Persona_.Commands.DeletePersona;
using HRA.Application.UseCases.Persona_.Commands.NewPersona;
using HRA.Application.UseCases.Persona_.Commands.UpdatePersona;
using HRA.Application.UseCases.Persona_.Queries.Lista_total_personas;
using HRA.Application.UseCases.Persona_.Queries.ListadoPersonas;
using HRA.Application.UseCases.Persona_.Queries.ObtenerPersona;
using HRA.Application.UseCases.Persona_.Queries.ObtenerPersonaXNDocument;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRA.WebAPI.Controllers.Bussines
{

    public class PersonaController : BaseController
    {


        /// <summary>
        /// Paginado Persona
        /// </summary>

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_persona([FromQuery] ListadoPersonasVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista total de personas
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_personas()
        {
            var r = await Mediator.Send(new PersonasVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista una persona
        /// </summary>

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_persona([FromQuery] PersonaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista una persona por numero de documento
        /// </summary>

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_persona_xndocumento([FromQuery] PersonaXNDocumentVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// crea una nueva persona
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_persona([FromBody] NewPersonaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Modifica una persona
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_persona([FromBody] UpdatePersonaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Activa una persona
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_persona([FromQuery] ActivatePersonaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Dar de baja una persona
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_persona([FromQuery] DeletePersonaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

    }
}
