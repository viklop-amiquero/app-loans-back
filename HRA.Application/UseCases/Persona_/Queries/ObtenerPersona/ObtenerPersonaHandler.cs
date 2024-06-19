using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using static HRA.Application.UseCases.Persona_.Queries.ObtenerPersona.PersonaDTO;

namespace HRA.Application.UseCases.Persona_.Queries.ObtenerPersona
{
    public class ObtenerPersonaHandler : IRequestHandler<PersonaVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Contacto> _repositoryContacto;
        private readonly IRepository<Contacto_emergencia> _repositoryContactoEm;
        private readonly IRepository<Puesto> _repositoryPuesto;
        private readonly IRepository<Puesto_persona> _repositoryPuestoPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Ubigeo> _repositoryUbigeo;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerPersonaHandler (
            IRepository<Persona> personaRepository,
            IRepository<Contacto> contactoRepository,
            IRepository<Contacto_emergencia> contactoEmRepository,
            IRepository<Puesto> puestoRepository,
            IRepository<Puesto_persona> puestoPersonaRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Ubigeo> ubigeoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryPersona = personaRepository;
            _repositoryContacto = contactoRepository;
            _repositoryContactoEm = contactoEmRepository;
            _repositoryPuesto = puestoRepository;
            _repositoryPuestoPersona = puestoPersonaRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryUbigeo = ubigeoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(PersonaVM request, CancellationToken cancellationToken)
        {

            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
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


            /// <summary>
            ///  obtener los datos de persona por Id
            /// </summary>
            int idPersona = request.I_PERSON_ID;
            var personaData = _repositoryPersona.TableNoTracking.Where(x => x.I_ID_PERSONA == idPersona).ToList();

            if (personaData.Count==0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "Cliente no encontrado")
                    }
                };
            }

            int idContactoEmergencia =_repositoryContactoEm.TableNoTracking
                 .Where(x => x.I_ID_PERSONA == idPersona && (x.B_ESTADO == "1"||x.B_ESTADO == "0"))
                 .FirstOrDefault()!.I_ID_CONTACTO_EM;

            int idUbigeo = _repositoryPersona.TableNoTracking
                 .Where(x => x.I_ID_PERSONA == idPersona && (x.B_ESTADO == "1" || x.B_ESTADO == "0"))
                 .FirstOrDefault()!.I_ID_UBIGEO;

            string codDistrict = _repositoryUbigeo.TableNoTracking.Where(x => x.I_ID_UBIGEO == idUbigeo).FirstOrDefault()!.V_CODIGO_DISTRITO!;

            var ubigeoData = _repositoryUbigeo.TableNoTracking.Where(x => x.I_ID_UBIGEO == idUbigeo && x.B_ESTADO == "1").ToList();

            var departamento = _repositoryUbigeo.TableNoTracking
                 .Where(x => x.V_DEPARTAMENTO != null && x.V_CODIGO_DEPARTAMENTO == ubigeoData.FirstOrDefault()!.V_CODIGO_DEPARTAMENTO && x.B_ESTADO == "1")
                 .FirstOrDefault()!.V_DEPARTAMENTO;

            var provincia = _repositoryUbigeo.TableNoTracking
                 .Where(x => x.V_PROVINCIA != null && x.V_CODIGO_PROVINCIA == ubigeoData.FirstOrDefault()!.V_CODIGO_PROVINCIA && x.B_ESTADO == "1")
                 .FirstOrDefault()!.V_PROVINCIA;

            var persona = new PersonaDTO()
            {
                I_PERSON_ID = (_repositoryPersona.TableNoTracking.Where(x => x.I_ID_PERSONA == idPersona).FirstOrDefault())?.I_ID_PERSONA,
                I_UBIGEO_ID = (personaData.FirstOrDefault())?.I_ID_UBIGEO,
                I_SEX_ID = (personaData.FirstOrDefault())?.I_ID_SEXO,
                V_FIRST_NAME = (personaData.FirstOrDefault())?.V_PRIMER_NOMBRE,
                V_SECOND_NAME = (personaData.FirstOrDefault())?.V_SEGUNDO_NOMBRE,
                V_PATERNAL_LAST_NAME = (personaData.FirstOrDefault())?.V_APELLIDO_PATERNO,
                V_MOTHER_LAST_NAME = (personaData.FirstOrDefault())?.V_APELLIDO_MATERNO,
                I_AGE = (personaData.FirstOrDefault())?.I_EDAD,
                D_BIRTHDATE = personaData.FirstOrDefault()?.D_FECHA_NACIMIENTO,
                V_ADDRESS_HOME = (personaData.FirstOrDefault())?.V_DIRECCION_DOMICILIO,
                V_ADDRESS_WORK = (personaData.FirstOrDefault())?.V_DIRECCION_TRABAJO,
                I_POSITION_ID = (_repositoryPuestoPersona.TableNoTracking.Where(x => x.I_ID_PERSONA == idPersona && x.B_ESTADO == "1").FirstOrDefault())?.I_ID_PUESTO,
                B_STATE = (personaData.FirstOrDefault())?.B_ESTADO,
                V_DEPARTMENT=departamento,
                V_PROVINCE=provincia,
                V_DISTRICT= ubigeoData.FirstOrDefault()!.V_DISTRITO,

                Document_persona =_repositoryPersona.TableNoTracking
                .Join(_repositoryDocPersona.TableNoTracking, P => P.I_ID_PERSONA, DP=>DP.I_ID_PERSONA,(P,DP)=>new {DP.I_ID_TIPO_DOC,DP.V_NRO_DOCUMENTO,DP.B_ESTADO,DP.I_ID_PERSONA})
                .Where(x => (x.B_ESTADO == "1"|| x.B_ESTADO == "0") && x.I_ID_PERSONA == request.I_PERSON_ID)
                 .Select(s => new document_persona
                 {
                     I_TYPE_DOCUMENT_ID = s.I_ID_TIPO_DOC,
                     V_NRO_DOCUMENT = s.V_NRO_DOCUMENTO,
                 }).ToList(),

                Contact = _repositoryContactoEm.TableNoTracking
                 .Join(_repositoryContacto.TableNoTracking, CE => CE.I_ID_CONTACTO_EM, C => C.I_ID_CONTACTO_EM, (CE, C) => new { C.I_ID_CONTACTO_EM, C.V_TELEFONO, C.V_CELULAR, C.V_CORREO, C.B_ESTADO })
                 .Where(x => (x.B_ESTADO == "1"|| x.B_ESTADO == "0") && x.I_ID_CONTACTO_EM == idContactoEmergencia)
                 .Select(s => new contacto_detail
                 {
                     V_MOVIL_PHONE = s.V_CELULAR,
                     V_PHONE = s.V_TELEFONO,
                     V_EMAIL = s.V_CORREO,
                 })
                 .ToList(),
                Contact_emergency = _repositoryPersona.TableNoTracking
                 .Join(_repositoryContactoEm.TableNoTracking, P => P.I_ID_PERSONA, CE => CE.I_ID_PERSONA, (P, CE) => new { CE.I_ID_PERSONA, CE.V_PARENTESCO, CE.V_NOMBRE,CE.V_CELULAR,CE.V_TELEFONO, P.B_ESTADO, })
                 .Where(x => (x.B_ESTADO == "1" || x.B_ESTADO == "0") && x.I_ID_PERSONA == idPersona)
                 .Select(s => new contacto_emergency
                 {
                     V_NAME_RELATIONSHIP = s.V_NOMBRE==null?null: s.V_NOMBRE.ToString() ,
                     V_RELATIONSHIP = s.V_PARENTESCO ==null ? null:s.V_PARENTESCO.ToString(),
                     V_MOVIL_PHONE_RELATIONSHIP = s.V_CELULAR == null ? null : s.V_CELULAR.ToString(),
                     V_PHONE_RELATIONSHIP = s.V_TELEFONO == null ? null : s.V_TELEFONO.ToString(),
                 })
                 .ToList(),


            };

            var map = _mapper.Map<PersonaDTO>(persona);

            if (map != null)
            {
                return new SuccessResult<PersonaDTO>(map);
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
