using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Ubigeo_.Queries.ObtenerUbigeo
{
    public class ObtenerUbigeoHandler : IRequestHandler<UbigeoVM, Iresult>
    {
        private readonly IRepository<Ubigeo> _repositoryUbigeo;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerUbigeoHandler(
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

        public async Task<Iresult> Handle(UbigeoVM request, CancellationToken cancellationToken)
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
            ///  obtener los datos de Ubigeo por Id
            /// </summary>


            int idUbigeo = request.I_UBIGEO_ID;
            var ubigeoData = _repositoryUbigeo.TableNoTracking.Where(x => x.I_ID_UBIGEO == idUbigeo && x.B_ESTADO=="1").ToList();

            if (ubigeoData.Count == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "Ubigeo no encontrado")
                    }
                };
            }
                
            var departamento = _repositoryUbigeo.TableNoTracking
                 .Where(x => x.V_DEPARTAMENTO != null && x.V_CODIGO_DEPARTAMENTO==ubigeoData.FirstOrDefault()!.V_CODIGO_DEPARTAMENTO && x.B_ESTADO == "1")
                 .FirstOrDefault()!.V_DEPARTAMENTO;

            var provincia = _repositoryUbigeo.TableNoTracking
                 .Where(x => x.V_PROVINCIA != null && x.V_CODIGO_PROVINCIA == ubigeoData.FirstOrDefault()!.V_CODIGO_PROVINCIA && x.B_ESTADO == "1")
                 .FirstOrDefault()!.V_PROVINCIA;

            var ubigeo = new UbigeoDTO()
            {

                I_UBIGEO_ID = ubigeoData.FirstOrDefault()!.I_ID_UBIGEO,
                V_DEPARTMENT_CODE = ubigeoData.FirstOrDefault()!.V_CODIGO_DEPARTAMENTO,
                V_DEPARTMENT = departamento,
                V_PROVINCE_CODE = ubigeoData.FirstOrDefault()!.V_CODIGO_PROVINCIA,
                V_PROVINCE = provincia,
                V_DISTRICT_CODE = ubigeoData.FirstOrDefault()!.V_CODIGO_DISTRITO,
                V_DISTRICT = ubigeoData.FirstOrDefault()!.V_DISTRITO,
                V_CAPITAL = ubigeoData.FirstOrDefault()!.V_CAPITAL,
                V_ALTITUDE = ubigeoData.FirstOrDefault()!.V_ALTITUDE,
                V_LATITUDE = ubigeoData.FirstOrDefault()!.V_LATITUDE,
                V_LONGITUDE = ubigeoData.FirstOrDefault()!.V_LONGITUDE,
                B_STATE = ubigeoData.FirstOrDefault()!.B_ESTADO,

            };

            var map = _mapper.Map<UbigeoDTO>(ubigeo);

            if (map != null)
            {
                return new SuccessResult<UbigeoDTO>(map);
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
