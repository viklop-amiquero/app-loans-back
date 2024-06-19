using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Interes_credito_.Queries.Listatotal_interescredito;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Interes_credito_.Queries.ObtenerInteresCredito
{
    public class ObtenerInteresCreditoHandler : IRequestHandler<TasaCreditoVM, Iresult>
    {
        private readonly IRepository<Interes_credito> _repositoryInteresCredito;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerInteresCreditoHandler (
            IRepository<Interes_credito> interesCreditoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryInteresCredito = interesCreditoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(TasaCreditoVM request, CancellationToken cancellationToken)
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
            int idInteres= request.I_INTEREST_ID;
            var interesData = _repositoryInteresCredito.TableNoTracking.Where(x => x.I_ID_INTERES_CREDITO == idInteres).ToList();

            if (interesData.Count==0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "Interes crédito no encontrado")
                    }
                };
            }

            var interesCredito = new InteresCreditoDTO()
            {
                I_INTEREST_CREDIT_ID =idInteres,
                V_NAME = (interesData.FirstOrDefault())!.V_NOMBRE,
                I_INTEREST = (interesData.FirstOrDefault())!.I_TASA_INTERES,
                V_FREQUENCY = (interesData.FirstOrDefault())!.V_FRECUENCIA,
                V_DESCRIPTION = (interesData.FirstOrDefault())?.V_DESCRIPCION,
                B_STATE = (interesData.FirstOrDefault())!.B_ESTADO
            };

            var map = _mapper.Map<InteresCreditoDTO>(interesCredito);

            if (map != null)
            {
                return new SuccessResult<InteresCreditoDTO>(map);
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
