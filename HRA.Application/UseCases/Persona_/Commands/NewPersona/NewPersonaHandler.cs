using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Persona_.Commands.NewPersona
{
    public class NewPersonaHandler : IRequestHandler<NewPersonaVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Contacto> _repositoryContacto;
        private readonly IRepository<Contacto_emergencia> _repositoryContactoEm;
        private readonly IRepository<Tipo_documento> _repositoryTipoDocumento;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewPersonaHandler(
            IRepository<Persona> personaRepository,
            IRepository<Contacto> contactoRepository,
            IRepository<Contacto_emergencia> contactoEmRepository,
            IRepository<Tipo_documento> tipoDocumentoRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor
            )
        {
            _repositoryPersona = personaRepository;
            _repositoryContacto = contactoRepository;
            _repositoryContactoEm = contactoEmRepository;
            _repositoryTipoDocumento= tipoDocumentoRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }
        public async Task<Iresult> Handle(NewPersonaVM request, CancellationToken cancellationToken)
        {

            var claims = _httpContextAccesor?.HttpContext?.User?.Claims;
            var claimUserId = claims?.FirstOrDefault(c => c.Type == "IDUser")?.Value;

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

            int idNuevaPersona = 0;
            int idNContactoEm = 0;
            DateTime fechaNacimiento = Convert.ToDateTime(request.D_BIRTH_DATE);

            //Calculando edad y validando
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > DateTime.Now.AddYears(-edad)) { 

              edad--;
            }


            if (fechaNacimiento>DateTime.Now)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 400,
                    Value = new List<DetailError>()
                    {
                        new DetailError("03", "Fecha de nacimiento incorrecto")
                    }
                };
            }

            if (_repositoryDocPersona.TableNoTracking.Where(x => x.V_NRO_DOCUMENTO == request.V_NUMBER_DOCUMENT).ToList().Count == 0)
            {
                var newPersona = new Persona { };
                if (request.I_STEP == 1)
                {
                    /// <summary>
                    /// insertar tabla persona
                    /// </summary>
                    newPersona =new Persona
                    {
                       
                        I_ID_SEXO = Convert.ToInt32(request.I_SEX_ID),
                        V_PRIMER_NOMBRE = request.V_FIRST_NAME.ToUpper(),
                        V_SEGUNDO_NOMBRE = request.V_SECOND_NAME == "" ? null : request.V_SECOND_NAME?.ToUpper(),
                        V_APELLIDO_PATERNO = request.V_PATERNAL_LAST_NAME.ToUpper(),
                        V_APELLIDO_MATERNO = request.V_MOTHER_LAST_NAME.ToUpper(),
                        I_EDAD = edad,
                        D_FECHA_NACIMIENTO = fechaNacimiento,
                        
                    };
                }
                request.I_STEP++;
                if (request.I_STEP == 2)
                {
                    /// <summary>
                    /// insertar tabla persona
                    /// </summary>
                    newPersona.I_ID_UBIGEO = Convert.ToInt32(request.I_UBIGEO_ID);
                    newPersona.V_DIRECCION_DOMICILIO = request.V_ADDRESS_HOME.ToUpper();
                    newPersona.V_DIRECCION_TRABAJO = request.V_ADDRESS_WORK == "" ? null : request.V_ADDRESS_WORK?.ToUpper();
                    newPersona.B_ESTADO = "1";
                    newPersona.I_USUARIO_CREACION = usuario.I_ID_USUARIO;
                    newPersona.D_FECHA_CREACION = _repositoryDate.Now;
                    
                    
                    _repositoryPersona.Insert(new List<Persona> { newPersona });

                    idNuevaPersona = newPersona.I_ID_PERSONA;

                    /// <summary>
                    /// insertando documento_persona
                    /// </summary>
                    if (request.I_TYPE_DOC_ID != 0)
                    {
                        _repositoryDocPersona.Insert(new List<Documento_persona>
                        {
                            new Documento_persona
                            {
                                I_ID_TIPO_DOC = request.I_TYPE_DOC_ID,
                                I_ID_PERSONA = idNuevaPersona,
                                V_NRO_DOCUMENTO = request.V_NUMBER_DOCUMENT,
                                B_ESTADO = "1",
                                I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                                D_FECHA_CREACION = _repositoryDate.Now
                            }
                        });
                    }

                    var newContactoEm = new Contacto_emergencia
                    {
                        I_ID_PERSONA = idNuevaPersona,
                        V_NOMBRE = request.V_NAME_RELATIONSHIP == "" ? null : request.V_NAME_RELATIONSHIP?.ToUpper(),
                        V_PARENTESCO = request.V_RELATIONSHIP == "" ? null : request.V_RELATIONSHIP?.ToUpper(),
                        V_CELULAR = request.V_MOVIL_PHONE_RELATIONSHIP == "" ? null : request.V_MOVIL_PHONE_RELATIONSHIP,
                        V_TELEFONO = request.V_PHONE_RELATIONSHIP == "" ? null : request.V_PHONE_RELATIONSHIP,
                        B_ESTADO = "1",
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now
                    };
                    _repositoryContactoEm.Insert(new List<Contacto_emergencia> { newContactoEm });
                    idNContactoEm = newContactoEm.I_ID_CONTACTO_EM;



                    var newContacto = new Contacto
                    {
                        I_ID_CONTACTO_EM = idNContactoEm,
                        V_TELEFONO = request.V_PHONE == "" ? null : request.V_PHONE,
                        V_CELULAR = request.V_MOVIL_PHONE,
                        V_CORREO = request.V_EMAIL == "" ? null : request.V_EMAIL?.ToUpper(),
                        B_ESTADO = "1",
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now
                    };
                    _repositoryContacto.Insert(new List<Contacto> { newContacto });
                }
                
                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Registro ya existente (dni) o registro inactivo")
                }
            };
        }
    }
}