using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.EntitiesStoreProcedure.SP_Cuota;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuota_.Queries.Listado_cuotas
{
    public class ListadoCuotasHandler : IRequestHandler<ListadoCuotasVM, Iresult>
    {
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListadoCuotasHandler(
            IRepository<Cuota> cuotaRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositoryCuota = cuotaRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        ///  obtener los valores desde el procedimiento almacenado USP_SEL_CUOTA
        ///  de acuerdo a los parametros enviados
        /// </summary>
        /// 
        public async Task<Iresult> Handle(ListadoCuotasVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccessor as ClaimContextAccessor;
            //var usuario = _repositoryCredito.Table.FirstOrDefault(x => x.V_NOMBRE == Convert.ToString(identity.UserName));

            //if (usuario == null)
            //{
            //    return new FailureResult<IEnumerable<DetailError>>()
            //    {
            //        StatusCode = 500,
            //        Value = new List<DetailError>()
            //        {
            //            new DetailError("05", "Usuario no autorizado")
            //        }
            //    };
            //}
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

            var sp_listado_cuota = await _unitOfWork.ExcuteStoreQueryList<entity_listado_cuota>("[rapidiario].[USP_SEL_CUOTA] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoCuotasDTO>>(sp_listado_cuota.Item1);
            var Grid = new ResultGrid<List<ListadoCuotasDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_cuota.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoCuotasDTO>>>(Grid);
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
