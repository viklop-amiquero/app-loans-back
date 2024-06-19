using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Ubigeo_.Queries.ListaTotalProvincia
{
    public class ListaTotalProvinciaHandler : IRequestHandler<ProvinciaVM, Iresult>
    {
        private readonly IRepository<Ubigeo> _repositoryUbigeo;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaTotalProvinciaHandler(
            IRepository<Ubigeo> ubigeoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUbigeo = ubigeoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(ProvinciaVM request, CancellationToken cancellationToken)
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
            if (request.V_DEPARTAMENT_CODE == "" )
            {
                return new SuccessResult<IEnumerable<DetailError>>();
            }
            string codDeparment = request.V_DEPARTAMENT_CODE!;
            var ubigeo = _repositoryUbigeo.TableNoTracking.Where(x => x.V_CODIGO_DEPARTAMENTO==codDeparment && 
            (x.V_CODIGO_PROVINCIA!=null && x.V_PROVINCIA!=null) && x.B_ESTADO=="1").ToList();

            var map = _mapper.Map<List<ProvinciaDTO>>(ubigeo);

            if (map != null)
            {
                return new SuccessResult<List<ProvinciaDTO>>(map);
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
