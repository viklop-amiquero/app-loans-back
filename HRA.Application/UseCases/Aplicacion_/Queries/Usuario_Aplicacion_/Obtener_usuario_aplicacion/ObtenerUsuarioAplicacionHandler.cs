using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Obtener_usuario_aplicacion
{
    public class ObtenerUsuarioAplicacionHandler : IRequestHandler<UsuarioAplicacionVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUserApp;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerUsuarioAplicacionHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Usuario_Aplicacion> userAppRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryUserApp = userAppRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(UsuarioAplicacionVM request, CancellationToken cancellationToken)
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

            var user = _repositoryUsuario.TableNoTracking.FirstOrDefault(x => x.I_ID_USUARIO == request.I_USER_ID /*&& x.B_ESTADO == "1"*/);

            if (user == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "El usuario no existe")
                    }
                };
            }

            var user_app = _repositoryUserApp.TableNoTracking.FirstOrDefault(x => x.I_ID_USUARIO == user.I_ID_USUARIO /*&& x.B_ESTADO == "1"*/);

            var map = _mapper.Map<UsuarioAplicacionDTO>(user_app);

            if (map != null)
            {
                map.V_USER = user.V_USUARIO;
                map.I_PERSON_ID = user.I_ID_PERSONA;
                map.I_ROLE_ID = user_app == null ? 0 : _repositoryAppRolMenu.TableNoTracking.FirstOrDefault(x => x.I_ID_APLICACION_ROL_MENU == user_app.I_ID_APLICACION_ROL_MENU
                                                                            && x.B_ESTADO == "1")!.I_ID_ROL;

                return new SuccessResult<UsuarioAplicacionDTO>(map);
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
