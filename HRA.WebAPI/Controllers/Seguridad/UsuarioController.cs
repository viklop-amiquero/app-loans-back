using HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.ActivateUsuario;
using HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.DeleteUsuario;
using HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.NewUsuario;
using HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.UpdateAccesoUsuario;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Lista_total_usuarios;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Listado_usuarios;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Obtener_pers_usuario;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Obtener_usuario;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Seguridad
{
    public class UsuarioController : BaseController
    {

        /// <summary>
        /// Paginado de usuarios
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_usuario([FromQuery] ListadoUsuariosVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista total de usuarios
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_usuarios()
        {
            var r = await Mediator.Send(new UsuariosVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un usuario
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_usuario([FromQuery] UsuarioVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un usuario mediante su nombre
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_usuario_pers([FromQuery] UsuarioPersVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Post_new_usuario([FromBody] NewUsuarioVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Actualiza el tiempo de acceso de un usuario
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_access_usuario([FromBody] UpdateAccesoUsuarioVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Dar de baja un usuario
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_usuario([FromQuery] DeleteUsuarioVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Activa un usuario
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_usuario([FromQuery] ActivateUsuarioVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
