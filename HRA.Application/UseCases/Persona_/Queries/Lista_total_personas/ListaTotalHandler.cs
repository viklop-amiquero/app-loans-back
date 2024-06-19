using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Persona_.Queries.Lista_total_personas
{
    public class ListaTotalHandler : IRequestHandler<PersonasVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Contacto> _repositoryContacto;
        private readonly IRepository<Contacto_emergencia> _repositoryContactoEm;
        private readonly IRepository<Puesto> _repositoryPuesto;
        private readonly IRepository<Puesto_persona> _repositoryPuestoPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaTotalHandler(
            IRepository<Persona> personaRepository,
            IRepository<Contacto> contactoRepository,
            IRepository<Contacto_emergencia> contactoEmRepository,
            IRepository<Puesto> puestoRepository,
            IRepository<Puesto_persona> puestoPersonaRepository,
            IRepository<Documento_persona> docPersonaRepository,
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
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(PersonasVM request, CancellationToken cancellationToken)
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

            var personaData = _repositoryDocPersona.TableNoTracking
            .Join(_repositoryPersona.TableNoTracking, DP => DP.I_ID_PERSONA, P => P.I_ID_PERSONA, (DP, P) => new { DP.V_NRO_DOCUMENTO, DP.B_ESTADO, DP.I_ID_PERSONA, P.V_PRIMER_NOMBRE, P.V_APELLIDO_PATERNO, P.V_APELLIDO_MATERNO })
            .Where(x => x.B_ESTADO == "1")
            .Select(s => new PersonasDTO
            {
                I_PERSON_ID = s.I_ID_PERSONA,
                V_NRO_DOCUMENT = s.V_NRO_DOCUMENTO,
                V_FIRST_NAME = s.V_PRIMER_NOMBRE,
                V_PATERNAL_LAST_NAME = s.V_APELLIDO_PATERNO,
                V_MOTHER_LAST_NAME = s.V_APELLIDO_MATERNO,
                B_STATE = s.B_ESTADO,
            }).ToList();

            var map = _mapper.Map<List<PersonasDTO>>(personaData);

            if (map != null)
            {
                
                return new SuccessResult<List<PersonasDTO>>(map);
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
