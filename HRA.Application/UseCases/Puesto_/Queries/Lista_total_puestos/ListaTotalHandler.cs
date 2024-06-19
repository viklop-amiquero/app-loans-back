using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Puesto_.Queries.Obtener_puesto;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Puesto_.Queries.Lista_total_puestos
{
    public class ListaTotalHandler : IRequestHandler<PuestosVM, Iresult>
    {
        private readonly IRepository<Puesto> _repositoryPuesto;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaTotalHandler(IRepository<Puesto> puestoRepository, IRepository<Usuario> usuarioRepository,
 IUnitOfWork unitOfWork, IDateTime dateTime, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repositoryPuesto = puestoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(PuestosVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccessor as ClaimContextAccessor;
            /*    var usuario = _repositoryUsuario.Table.FirstOrDefault(x => x.V_USUARIO == Convert.ToString(identity.UserName));

                if (usuario == null)
                {
                    return new FailureResult<IEnumerable<DetailError>>()
                    {
                        StatusCode = 500,
                        Value = new List<DetailError>()
                        {
                            new DetailError("05", "Usuario no autorizado")
                        }
                    };
                }*/

            var puesto = _repositoryPuesto.TableNoTracking.ToList();
            var map = _mapper.Map<List<PuestoDTO>>(puesto);

            if (map != null)
            {
                return new SuccessResult<List<PuestoDTO>>(map);
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
