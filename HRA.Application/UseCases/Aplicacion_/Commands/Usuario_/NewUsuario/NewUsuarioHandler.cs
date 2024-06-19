using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure;
using HRA.Transversal.mail_provider;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.NewUsuario
{
    public class NewUsuarioHandler : IRequestHandler<NewUsuarioVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Clave> _repositoryPassword;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Isend_mail _sent_mail;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewUsuarioHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Persona> personaRepository,
            IRepository<Documento_persona> docPersRepository,
            IRepository<Clave> passwordRepository,
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            Isend_mail sent_mail,
            IWebHostEnvironment webHostEnvironment)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryPersona = personaRepository;
            _repositoryDocPersona = docPersRepository;
            _repositoryPassword = passwordRepository;
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sent_mail = sent_mail;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Iresult> Handle(NewUsuarioVM request, CancellationToken cancellationToken)
        {
            var Claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            // Solo el admin puede crear usuarios
            var usuario = _repositoryUsuario.TableNoTracking
                .Where(x => x.B_ESTADO == "1" && x.I_ID_USUARIO == Convert.ToInt32(claimUserId)).FirstOrDefault();

            // Cualquier persona registrada puede crear usuarios
            //var usuario = _repositoryPersona.TableNoTracking
            //    .Where(x => x.B_ESTADO == "1" && x.V_DNI == request.V_NAME).FirstOrDefault();

            if (usuario is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 406,
                    Value = new List<DetailError>()
                    {
                        new DetailError("03", "Usuario no autorizado")
                    }
                };
            }

            var persona = _repositoryPersona.TableNoTracking.FirstOrDefault(x => x.I_ID_PERSONA.ToString() == request.I_PERSON_ID && x.B_ESTADO == "1");

            if (persona is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 406,
                    Value = new List<DetailError>()
                    {
                        new DetailError("03", "La persona no existe")
                    }
                };
            }

            // Limite a 1 usuario por persona
            if (_repositoryUsuario.TableNoTracking.Where(x => x.I_ID_PERSONA.ToString() == request.I_PERSON_ID && x.B_ESTADO == "1").ToList().Count == 1)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 400,
                    Value = new List<DetailError>()
                    {
                        new DetailError("06", "La persona ya tiene un registro de usuario existente y activo")
                    }
                };
            }

            // Consultar al ing si solo el admin creara a los usuarios y si es necesario pedir el nombre y pass cuando se puede asignar automaticamente
            // el DNI como nombre de usuario y una contra aleatoria por defecto
            //var password = await _unitOfWork.StoreQuery<entity_Encrypt>("SELECT [seguridad].[UFN_ENCRYPT]({0}) AS B_CHAIN", request.V_PASS);

            // Si la persona existe, si o si tendra un numero de documento
            var doc = _repositoryDocPersona.TableNoTracking.FirstOrDefault(x => x.I_ID_PERSONA == persona.I_ID_PERSONA)!.V_NRO_DOCUMENTO;
            var pass_encrypt = await _unitOfWork.StoreQuery<entity_Encrypt>("SELECT [seguridad].[UFN_ENCRYPT]({0}) AS B_CHAIN", String.Concat("Rd", doc, "@"));

            var user = new Usuario
            {
                I_ID_PERSONA = persona.I_ID_PERSONA,
                V_USUARIO = doc,
                B_ESTADO = "1",
                I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                D_FECHA_CREACION = _repositoryDate.Now,
            };
            _repositoryUsuario.Insert(new List<Usuario> { user });

            _repositoryPassword.Insert(new List<Clave>
            {
                new Clave
                {
                    I_ID_USUARIO = user.I_ID_USUARIO,
                    V_CLAVE_HASH = pass_encrypt.FirstOrDefault()!.B_CHAIN,
                    B_ESTADO = "1",
                    I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                    D_FECHA_CREACION = _repositoryDate.Now
                }
            });

            var app_rol = _repositoryAppRolMenu.TableNoTracking.Where(x => x.I_ID_ROL.ToString() == request.I_ROLE_ID && x.B_ESTADO == "1").ToList();

            if (app_rol.Count() == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existen menús para este rol. Asigne menús para continuar")
                    }
                };
            }

            app_rol.ForEach(apm =>
            {
                _repositoryUsuarioApp.Insert(new List<Usuario_Aplicacion>
                {
                    new Usuario_Aplicacion
                    {
                        I_ID_USUARIO = user.I_ID_USUARIO,
                        I_ID_APLICACION_ROL_MENU = apm.I_ID_APLICACION_ROL_MENU,
                        D_FECHA_INICIO = Convert.ToDateTime(request.D_START_DATE).Date,
                        //D_FECHA_FIN = request.D_END_DATE == "" ? null : request.D_END_DATE == "null" ? null : Convert.ToDateTime(request.D_END_DATE).Date,
                        D_FECHA_FIN = Convert.ToDateTime(request.D_END_DATE).Date,
                        //B_ESTADO = request.D_END_DATE == "" || request.D_END_DATE == "null" ? "1" : "0",
                        B_ESTADO = "1",
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now,
                    }
                });
            });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);

            //if (_repositoryUsuario.TableNoTracking.Where(x => x.I_ID_PERSONA.ToString() == request.I_PERSON_ID && x.B_ESTADO == "1").ToList().Count == 0)
            //{
            //    _repositoryUsuario.Insert(new List<Usuario>
            //    {
            //        new Usuario
            //        {
            //            I_ID_PERSONA = Convert.ToInt32(request.I_PERSON_ID),
            //            V_USUARIO = request.V_NAME,
            //            B_ESTADO = "1",
            //            I_USUARIO_CREACION = usuario.I_ID_USUARIO,
            //            D_FECHA_CREACION = _repositoryDate.Now,
            //        }
            //    });

            //    var id_user = _repositoryUsuario.TableNoTracking.Where(x => x.V_USUARIO == request.V_NAME && x.B_ESTADO == "1").Select(x => x.I_ID_USUARIO).FirstOrDefault();
            //    _repositoryPassword.Insert(new List<Clave>
            //    {
            //        new Clave
            //        {
            //            I_ID_USUARIO = id_user,
            //            V_CLAVE_HASH = password.FirstOrDefault().B_CHAIN,
            //            B_ESTADO = "1",
            //            I_USUARIO_CREACION = usuario.I_ID_USUARIO,
            //            D_FECHA_CREACION = _repositoryDate.Now
            //        }
            //    });

            //    await _unitOfWork.CommitChanges();
            //    return new SuccessResult<Unit>(Unit.Value);
            //}

            //return new FailureResult<IEnumerable<DetailError>>()
            //{
            //    StatusCode = 400,
            //    Value = new List<DetailError>()
            //    {
            //        new DetailError("06", "Registro ya existente (nombre) o registro inactivo")
            //    }
            //};
        }
    }
}
