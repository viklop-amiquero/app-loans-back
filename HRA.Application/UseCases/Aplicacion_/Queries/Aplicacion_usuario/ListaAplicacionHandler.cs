using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_menu;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using HRA.Transversal.tokenProvider;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_usuario
{
    public class ListaAplicacionHandler : IRequestHandler<ListaAplicacionVM, Iresult>
    {
        private readonly IRepository<Aplicacion> _repositoryAplicacion;
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUser_aplicacion;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Rol> _repositoryRol;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GenerateToken _authenticationToken;
        public ListaAplicacionHandler(
            IRepository<Aplicacion> aplicacionRepository, 
            IRepository<Menú> menuRepository,
            IRepository<Usuario_Aplicacion> usuario_appRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Persona> personaRepository, 
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Usuario> usuarioRepository, 
            IRepository<Rol> rolRepository,
            IHttpContextAccessor httpContextAccessor,
            GenerateToken generate)
        {
            _repositoryAplicacion = aplicacionRepository;
            _repositoryMenu = menuRepository;
            _repositoryUser_aplicacion = usuario_appRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryPersona = personaRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryRol = rolRepository;
            _httpContextAccessor = httpContextAccessor;
            _authenticationToken = generate;
        }

        public async Task<Iresult> Handle(ListaAplicacionVM request, CancellationToken cancellationToken)
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

            var persona = _repositoryPersona.TableNoTracking.ToList()
                .Join(_repositoryUsuario.TableNoTracking.ToList(), P => P.I_ID_PERSONA, U => U.I_ID_PERSONA, (P, U) => new { P, U }).Where(w => w.P.B_ESTADO == "1" && w.U.I_ID_USUARIO == request.I_USER_ID)
                .Join(_repositoryDocPersona.TableNoTracking.ToList(), PU => PU.P.I_ID_PERSONA, DP => DP.I_ID_PERSONA, (PU, DP) => new { PU, DP }).Where(w => w.DP.B_ESTADO == "1")
                .Select(s => new
                {
                    s.PU.P.I_ID_PERSONA,
                    s.DP.V_NRO_DOCUMENTO,
                    s.PU.P.V_PRIMER_NOMBRE,
                    s.PU.P.V_SEGUNDO_NOMBRE,
                    s.PU.P.V_APELLIDO_PATERNO,
                    s.PU.P.V_APELLIDO_MATERNO
                })
                .ToList().SingleOrDefault();

            var application = _repositoryUser_aplicacion.TableNoTracking.ToList()
                .Join(_repositoryAppRolMenu.TableNoTracking.ToList(), UA => UA.I_ID_APLICACION_ROL_MENU, ARM => ARM.I_ID_APLICACION_ROL_MENU, (UA, ARM) => new { UA, ARM }).Where(w => w.UA.B_ESTADO == "1" && w.UA.I_ID_USUARIO == request.I_USER_ID)
                .Join(_repositoryRol.TableNoTracking.ToList(), UARM => UARM.ARM.I_ID_ROL, R => R.I_ID_ROL, (UARM, R) => new { UARM, R }).Where(w => w.R.B_ESTADO == "1")
                .Join(_repositoryMenu.TableNoTracking.ToList(), UAARM => UAARM.UARM.ARM.I_ID_MENU, M => M.I_ID_MENU, (UAARM, M) => new { UAARM, M }).Where(w => w.M.B_ESTADO == "1")
                .Join(_repositoryAplicacion.TableNoTracking.ToList(), UAARMM => UAARMM.M.I_ID_APLICACION, A => A.I_ID_APLICACION, (UAARMM, A) => new { UAARMM, A }).Where(w => w.A.B_ESTADO == "1")
                .Select(s => new ListaAplicacionDTO
            {
                Aplicaciones = new app
                {
                    id_app = s.A.I_ID_APLICACION.ToString(),
                    application = s.A.V_APLICACION,
                    acronym = s.A.V_ACRONIMO,
                    url = s.A.V_URL,
                    id_persona = persona.I_ID_PERSONA.ToString(),
                    id_rol = s.UAARMM.UAARM.R.I_ID_ROL.ToString(),
                    rol = s.UAARMM.UAARM.R.V_ROL,
                    fecha_inicio = s.UAARMM.UAARM.UARM.UA.D_FECHA_INICIO.ToString(),
                    fecha_fin = s.UAARMM.UAARM.UARM.UA.D_FECHA_FIN.ToString()
                },
                token = _authenticationToken.GenerarToken<token>(new List<token>
                {
                    new token
                    {
                        id_app = s.A.I_ID_APLICACION.ToString(),
                        id_persona = persona.I_ID_PERSONA.ToString(),
                        id_user = usuario.I_ID_USUARIO.ToString(),
                        dniUser = persona.V_NRO_DOCUMENTO,
                        namesUser = persona.V_PRIMER_NOMBRE+" "+persona.V_APELLIDO_PATERNO+" "+persona.V_APELLIDO_MATERNO,
                        application = s.A.V_APLICACION,
                        acronym = s.A.V_ACRONIMO,
                        id_rol = s.UAARMM.UAARM.R.I_ID_ROL.ToString(),
                        Role = s.UAARMM.UAARM.R.V_ROL
                    }
                })
            }).ToList();

            if (application != null)
            {
                return new SuccessResult<List<ListaAplicacionDTO>>(application);
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
