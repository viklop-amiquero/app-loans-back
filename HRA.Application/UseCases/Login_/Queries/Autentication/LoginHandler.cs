using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Login_.Queries.Autentication;
using HRA.Application.UseCases.Login_.Queries.ExpiredAccess;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Logger;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure;
using HRA.Transversal.tokenProvider;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Login_.Queries.Authentication
{
    public class LoginHandler : IRequestHandler<LoginVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUser;
        private readonly IRepository<Clave> _repositoryClave;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUserApp;
        private readonly IRepository<Login> _repositoryLogLogin;
        private readonly IDateTime _repositoryDate;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GenerateToken _authenticationToken;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public LoginHandler(
            IRepository<Usuario> userRepository,
            IRepository<Clave> claveRepository,
            IRepository<Usuario_Aplicacion> userAppRepository,
            IRepository<Login> log_loginRepository,
            IMapper mapper, IUnitOfWork unitOfWork,
            GenerateToken generate,
            IHttpContextAccessor httpContextAccesor,
            IDateTime dateTime)
        {
            _repositoryUser = userRepository;
            _repositoryClave = claveRepository;
            _repositoryUserApp = userAppRepository;
            _repositoryLogLogin = log_loginRepository;
            _repositoryDate = dateTime;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _authenticationToken = generate;
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task<Iresult> Handle(LoginVM request, CancellationToken cancellationToken)
        {
            var Claims = _httpContextAccesor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            object[] parameters = {
                request.V_USER,
                request.V_PASSWORD
            };

            var usuario = _repositoryUser.Table.FirstOrDefault(x => x.V_USUARIO == request.V_USER);

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

            if (usuario.B_ESTADO == "2")
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("04", "El usuario esta bloqueado")
                    }
                };
            }

            if (usuario.B_ESTADO == "0")
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("04", "El usuario esta inactivo")
                    }
                };
            }

            // Esta variable solo sirve para ver cuantos registros cumplen esa condicion (eliminar despues)
            var usersAccess = _repositoryUserApp.TableNoTracking.Where(x => x.B_ESTADO == "1" && x.D_FECHA_FIN < _repositoryDate.Today).ToList();

            if (_repositoryUserApp.TableNoTracking.Where(x => x.B_ESTADO == "1" && x.D_FECHA_FIN < _repositoryDate.Today).ToList().Count() != 0)
            {
                //Ejecutar SP
                var sp_expired_access = _mapper.Map<List<ExpiredAccessDTO>>(await _unitOfWork.ExcuteStoreQuery<entity_Expired_access>("[seguridad].[USP_DELETE_USER_ACCESS_EXPIRED]"));
            }

            // Error para el acceso caducado de un usuario
            // Esta variable solo sirve para ver cuantos registros cumplen esa condicion (eliminar despues)
            var sp_prueba = _repositoryUserApp.TableNoTracking.Where(x => x.I_ID_USUARIO == usuario.I_ID_USUARIO && x.B_ESTADO == "3"
                                                            && x.D_FECHA_FIN < _repositoryDate.Today).ToList();

            if (_repositoryUserApp.TableNoTracking.Where(x => x.I_ID_USUARIO == usuario.I_ID_USUARIO && x.B_ESTADO == "3" 
                                                            && x.D_FECHA_FIN < _repositoryDate.Today).ToList().Count() != 0)
            { //if(usuario.B_ESTADO == "3")
                await _unitOfWork.CommitChanges();

                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("04", "Su acceso al sistema ha expirado. Contacte con el administrador para renovar su acceso")
                    }
                };
            }

            var map = _mapper.Map<List<LoginDTO>>(await _unitOfWork.ExcuteStoreQuery<entity_Login>("[seguridad].[USP_CONSULTA_ACCESO] {0}, {1}", parameters));
            var conteo = 0;

            if (!map.Any())
            {
                _repositoryLogLogin.Insert(new List<Login>
                {
                    new Login
                    {
                        I_ID_USER = usuario.I_ID_USUARIO,
                        V_LOGIN = usuario.V_USUARIO,
                        V_ID_ROL = map.Select(s => s.Role).FirstOrDefault(),
                        I_INTENTO = 1,
                        D_FECHA_REGISTRO = _repositoryDate.Now
                    }
                });

                var login_correcto = _repositoryLogLogin.TableNoTracking.OrderByDescending(x => x.D_FECHA_REGISTRO)
                .FirstOrDefault(w => w.V_LOGIN == usuario.V_USUARIO && w.I_INTENTO == 0 && w.D_FECHA_REGISTRO.Date == _repositoryDate.Now.Date);

                var cont_inc_log = _repositoryLogLogin.TableNoTracking.Where(w => w.V_LOGIN == usuario.V_USUARIO && w.I_INTENTO == 1
                                    && w.D_FECHA_REGISTRO.Date == _repositoryDate.Now.Date);

                if (login_correcto is not null)
                {
                    cont_inc_log = _repositoryLogLogin.TableNoTracking.Where(w => w.V_LOGIN == usuario.V_USUARIO && w.I_INTENTO == 1
                                    && w.D_FECHA_REGISTRO.Date == _repositoryDate.Now.Date && (w.D_FECHA_REGISTRO.CompareTo(login_correcto.D_FECHA_REGISTRO) > 0));
                }

                conteo = cont_inc_log.Count();

                if (conteo >= 5)
                {
                    usuario.B_ESTADO = "2";
                    usuario.I_USUARIO_MODIFICA = Convert.ToInt32(claimUserId);
                    usuario.D_FECHA_MODIFICA = _repositoryDate.Now;

                    var clave_actual = _repositoryClave.Table.FirstOrDefault(x => x.I_ID_USUARIO == usuario.I_ID_USUARIO && x.B_ESTADO == "1");
                    clave_actual.B_ESTADO = "0";
                    clave_actual.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                    clave_actual.D_FECHA_MODIFICA = _repositoryDate.Now;

                    _repositoryUserApp.Table.Where(x => x.I_ID_USUARIO == usuario.I_ID_USUARIO && x.B_ESTADO == "1").ToList().ForEach(u =>
                    {
                        u.B_ESTADO = "0";
                        u.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        u.D_FECHA_MODIFICA = _repositoryDate.Now;
                    });
                }

                await _unitOfWork.CommitChanges();

                if (conteo >= 5)
                {
                   return new FailureResult<IEnumerable<DetailError>>()
                    {
                        Value = new List<DetailError>()
                        {
                            new DetailError("04","El usuario se bloqueo por exceso de intentos")
                        }
                    };

                }

                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("03","Usuario y/o contraseña incorrectos")
                    }
                };
            }

            var Token = _authenticationToken.GenerarToken<LoginDTO>(map);

            _repositoryLogLogin.Insert(new List<Login>
            {
                new Login
                {
                    I_ID_USER = map.First().IDUser,
                    V_LOGIN = map.First().User_name,
                    V_ID_ROL = map.First().Role,
                    I_INTENTO = 0,
                    D_FECHA_REGISTRO = _repositoryDate.Now
                }
            });

            await _unitOfWork.CommitChanges();

            if (Token != null)
            {
                return new SuccessResult<string>(Token);
            }
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
