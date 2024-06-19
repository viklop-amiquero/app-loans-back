using HRA.Application.UseCases.Aplicacion_.Commands.Rol_.ActivateRol;
using HRA.Application.UseCases.Aplicacion_.Commands.Rol_.DeleteRol;
using HRA.Application.UseCases.Aplicacion_.Commands.Rol_.NewRol;
using HRA.Application.UseCases.Aplicacion_.Commands.Rol_.UpdateRol;
using HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Listado_menus_rol;
using HRA.Application.UseCases.Aplicacion_.Queries.Menu_rol;
using HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Lista_total_roles;
using HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Listado_roles;
using HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Obtener_rol;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Listado_usuarios_rol;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Seguridad
{
    public class RolController : BaseController
    {
        /// <summary>
        /// Paginado de roles
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_rol([FromQuery] ListadoRolesVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista total de roles
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_roles()
        {
            var r = await Mediator.Send(new RolesVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista de usuarios asignados a un rol
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_usuario_rol([FromQuery] UsuarioRolVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista de menús asignados a un rol
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_menu_rol([FromQuery] RolMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista para la vista de todos los menús y permisos de un rol
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_detalle_menu_rol([FromQuery] MenuRolVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un rol
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_rol([FromQuery] RolVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea un nuevo rol
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_rol([FromBody] NewRolVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica un rol
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_rol([FromBody] UpdateRolVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }            

        /// <summary>
        /// Dar de baja un rol
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_rol([FromQuery] DeleteRolVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Activa un rol
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_rol([FromQuery] ActivateRolVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
