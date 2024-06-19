using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure.SP_Cuenta;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuenta_.Queries.PaginadoCuentas
{
    public class PaginadoCuentasHandler : IRequestHandler<PaginadoCuentasVM, Iresult>
    {
        private readonly IRepository<Cuenta> _repositoryCuenta;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public PaginadoCuentasHandler(
            IRepository<Cuenta> cuentaRepository,
            IRepository<Usuario> usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositoryCuenta = cuentaRepository;
            _repositoryUsuario = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  obtener los datos de cuenta
        /// </summary>
        /// 
        public async Task<Iresult> Handle(PaginadoCuentasVM request, CancellationToken cancellationToken)
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

            request.I_PAGE_NUMBER ??= 1;
            request.I_PAGE_SIZE ??= 10;

            object[] parameters = {
                request.I_PAGE_NUMBER,
                request.I_PAGE_SIZE,
                request.V_FILTER_TYPE,
                request.V_FILTER_VALUE,
                request.I_SORT_BY_FIELD,
                request.V_SORT_ORDER,
            };

            var sp_paginado_cuenta = await _unitOfWork.ExcuteStoreQueryList<entity_paginado_cuenta>("[rapidiario].[USP_SEL_CUENTA_PAGINADO] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<PaginadoCuentasDTO>>(sp_paginado_cuenta.Item1);
            var Grid = new ResultGrid<List<PaginadoCuentasDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_paginado_cuenta.Item1.Count(),
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<PaginadoCuentasDTO>>>(Grid);
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