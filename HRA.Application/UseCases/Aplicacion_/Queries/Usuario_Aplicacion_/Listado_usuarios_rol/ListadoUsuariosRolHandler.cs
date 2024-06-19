using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Obtener_usuario_aplicacion;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Listado_usuarios_rol
{
    public class ListadoUsuariosRolHandler : IRequestHandler<UsuarioRolVM, Iresult>
    {
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListadoUsuariosRolHandler(
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UsuarioRolVM request, CancellationToken cancellationToken)
        {
            var Claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            var usuario = _repositoryUsuario.TableNoTracking
                .Where(x => x.B_ESTADO == "1" && x.I_ID_USUARIO == Convert.ToInt32(claimUserId)).FirstOrDefault();

            if (usuario is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("05", "Usuario no autorizado")
                    }
                };
            }

            var app_rol = _repositoryAppRolMenu.TableNoTracking.Where(x => x.I_ID_ROL == request.I_ROLE_ID && x.B_ESTADO == "1").ToList();

            var user_app = new List<Usuario_Aplicacion>();

            if (app_rol.Count() != 0)
            {
                //return new FailureResult<IEnumerable<DetailError>>()
                //{
                //    StatusCode = 500,
                //    Value = new List<DetailError>()
                //    {
                //        new DetailError("02", "No existen menús para este rol")
                //    }
                //};

                user_app = _repositoryUsuarioApp.TableNoTracking.Where(x => x.I_ID_APLICACION_ROL_MENU == app_rol[0].I_ID_APLICACION_ROL_MENU).ToList();
            }

            // Usuarios que tienen el rol ingresado

            var map = _mapper.Map<List<UsuarioAplicacionDTO>>(user_app);

            if (map != null)
            {
                map.ForEach(m =>
                {
                    m.V_USER = _repositoryUsuario.TableNoTracking.FirstOrDefault(x => x.I_ID_USUARIO == m.I_USER_ID)!.V_USUARIO;
                    m.I_PERSON_ID =  _repositoryUsuario.TableNoTracking.FirstOrDefault(x => x.I_ID_USUARIO == m.I_USER_ID)!.I_ID_PERSONA;
                    m.I_ROLE_ID = request.I_ROLE_ID;
                });

                return new SuccessResult<List<UsuarioAplicacionDTO>>(map);
            }
            else
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("01", "No se pudo obtener respuesta.")
                    }
                };
            }
        }
    }
}
