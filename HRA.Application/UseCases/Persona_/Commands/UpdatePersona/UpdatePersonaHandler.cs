using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Persona_.Commands.UpdatePersona
{
    public class UpdatePersonaHandler : IRequestHandler<UpdatePersonaVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Contacto> _repositoryContacto;
        private readonly IRepository<Contacto_emergencia> _repositoryContactoEm;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePersonaHandler(
            IRepository<Persona> personaRepository,
            IRepository<Contacto> contactoRepository,
            IRepository<Contacto_emergencia> contactoEmRepository,
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
            _repositoryDocPersona = docPersonaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }
        public async Task<Iresult> Handle(UpdatePersonaVM request, CancellationToken cancellationToken)
        {
            int edad = 0;
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


            var entity = _repositoryPersona.Table.FirstOrDefault(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "1");
            var entityDocPersona = _repositoryDocPersona.Table.FirstOrDefault(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe la persona o está inactivo")
                    }
                };

            }
            if (request.D_BIRTH_DATE!= "")
            {
                DateTime fechaNacimiento = Convert.ToDateTime(request.D_BIRTH_DATE);
                //Calculo edad
                edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (fechaNacimiento > DateTime.Now)
                {
                    return new FailureResult<IEnumerable<DetailError>>()
                    {
                        Value = new List<DetailError>()
                        {
                            new DetailError("03", "Fecha de nacimiento incorrecto")
                        }
                    };
                }

            }
            if(entityDocPersona.I_ID_TIPO_DOC!= request.I_TYPE_DOC_ID)
            {
                entityDocPersona.V_NRO_DOCUMENTO = request.V_NUMBER_DOCUMENT;
                entityDocPersona.I_ID_TIPO_DOC = request.I_TYPE_DOC_ID;
                _repositoryDocPersona.Update(entityDocPersona);
            }

            if (entityDocPersona?.V_NRO_DOCUMENTO==request.V_NUMBER_DOCUMENT || request.V_NUMBER_DOCUMENT == "")
            {
               
                if (request.I_STEP == 1)
                {
                    if (_repositoryDocPersona.TableNoTracking.Where(x =>x.V_NRO_DOCUMENTO == request.V_NUMBER_DOCUMENT  && x.I_ID_PERSONA != request.I_PERSON_ID).ToList().Count == 0)
                    {
                        /// <summary>
                        /// update tabla documento_persona
                        /// </summary>

                        entityDocPersona!.V_NRO_DOCUMENTO = request.V_NUMBER_DOCUMENT == "" ? entityDocPersona.V_NRO_DOCUMENTO : request.V_NUMBER_DOCUMENT;
                        entityDocPersona.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        entityDocPersona.D_FECHA_MODIFICA = _repositoryDate.Now;


                        /// <summary>
                        /// update tabla persona
                        /// </summary>
                        entity.I_ID_SEXO = request.V_SEX_ID == "" || request.V_SEX_ID == null ?entity.I_ID_SEXO : Convert.ToInt32(request.V_SEX_ID);
                        entity.V_PRIMER_NOMBRE = request.V_FIRST_NAME == "" ? entity.V_PRIMER_NOMBRE : request.V_FIRST_NAME?.ToUpper();
                        entity.V_SEGUNDO_NOMBRE = request.V_SECOND_NAME == "" ? entity.V_SEGUNDO_NOMBRE : request.V_SECOND_NAME?.ToUpper();
                        entity.V_APELLIDO_PATERNO = request.V_PATERNAL_LAST_NAME == "" ? entity.V_APELLIDO_PATERNO : request.V_PATERNAL_LAST_NAME?.ToUpper();
                        entity.V_APELLIDO_MATERNO = request.V_MOTHER_LAST_NAME == "" ? entity.V_APELLIDO_MATERNO : request.V_MOTHER_LAST_NAME?.ToUpper();
                        entity.I_EDAD = request.D_BIRTH_DATE == "" ? entity.I_EDAD : edad;
                        entity.D_FECHA_NACIMIENTO = request.D_BIRTH_DATE == "" ? entity.D_FECHA_NACIMIENTO : Convert.ToDateTime(request.D_BIRTH_DATE);

                    }
                }
                    request.I_STEP++;
                if (request.I_STEP == 2)
                {
                    if (_repositoryDocPersona.TableNoTracking.Where(x => x.V_NRO_DOCUMENTO == request.V_NUMBER_DOCUMENT && x.I_ID_PERSONA != request.I_PERSON_ID).ToList().Count == 0){

                        entity.I_ID_UBIGEO = request.V_UBIGEO_ID == "" || request.V_UBIGEO_ID == null ? entity.I_ID_UBIGEO : Convert.ToInt32(request.V_UBIGEO_ID);
                        entity.V_DIRECCION_DOMICILIO = request.V_ADDRESS_HOME == "" ? entity.V_DIRECCION_DOMICILIO : request.V_ADDRESS_HOME?.ToUpper();
                        entity.V_DIRECCION_TRABAJO = request.V_ADDRESS_WORK == "" ? "" : request.V_ADDRESS_WORK?.ToUpper();
                        entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        entity.D_FECHA_MODIFICA = _repositoryDate.Now;
                    }
                    /// <summary>
                    /// update tabla contacto_emergencia
                    /// </summary>

                    var entityCE = _repositoryContactoEm.Table.FirstOrDefault(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "1");

                    if (entityCE != null)
                    {
                        entityCE.V_NOMBRE = request.V_NAME_RELATIONSHIP == "" ? "" : request.V_NAME_RELATIONSHIP?.ToUpper();
                        entityCE.V_PARENTESCO = request.V_RELATIONSHIP == "" ? "" : request.V_RELATIONSHIP?.ToUpper();
                        entityCE.V_CELULAR = request.V_MOVIL_PHONE_RELATIONSHIP == "" ? "" : request.V_MOVIL_PHONE_RELATIONSHIP;
                        entityCE.V_TELEFONO = request.V_PHONE_RELATIONSHIP == "" ? "" : request.V_PHONE_RELATIONSHIP;
                        entityCE.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        entityCE.D_FECHA_MODIFICA = _repositoryDate.Now;

                    }
                    /// <summary>
                    /// update tabla contacto_emergencia
                    /// </summary>
                    var entityC = _repositoryContacto.Table.FirstOrDefault(x => x.I_ID_CONTACTO_EM == entityCE!.I_ID_CONTACTO_EM && x.B_ESTADO == "1");
                    if (entityC != null)
                    {
                        entityC.V_TELEFONO = request.V_PHONE == "" ? "" : request.V_PHONE;
                        entityC.V_CELULAR = request.V_MOVIL_PHONE == "" ? entityC.V_CELULAR : request.V_MOVIL_PHONE;
                        entityC.V_CORREO = request.V_EMAIL == "" ? "" : request.V_EMAIL?.ToUpper();
                        entityC.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        entityC.D_FECHA_MODIFICA = _repositoryDate.Now;
                    }
                }

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);


            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Registro ya existente")
                }
            };
        }
    }
}
