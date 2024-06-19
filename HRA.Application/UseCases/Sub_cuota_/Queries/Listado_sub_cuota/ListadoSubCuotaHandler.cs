using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Sub_cuota_.Queries.Listado_cuota;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.EntitiesStoreProcedure.SP_SubCuota;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Sub_cuota_.Queries.Listado_sub_cuota
{
    public class ListadoSubCuotasHandler : IRequestHandler<ListadoSubCuotaVM, Iresult>
    {
        private readonly IRepository<Sub_cuota> _repositorySubCuota;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListadoSubCuotasHandler(
            IRepository<Sub_cuota> subCuotaRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositorySubCuota = subCuotaRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        ///  obtener los valores desde el procedimiento almacenado USP_SEL_CUOTA
        ///  de acuerdo a los parametros enviados
        /// </summary>
        /// 
        public async Task<Iresult> Handle(ListadoSubCuotaVM request, CancellationToken cancellationToken)
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
                request.I_SORT_BY_FIELD??1,
                request.V_SORT_ORDER??"DESC",
            };

            var sp_listado_sub_cuota = await _unitOfWork.ExcuteStoreQueryList<entity_listado_sub_cuota>("[rapidiario].[USP_SEL_SUB_CUOTA] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoSubCuotaDTO>>(sp_listado_sub_cuota.Item1);
            var Grid = new ResultGrid<List<ListadoSubCuotaDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_sub_cuota.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoSubCuotaDTO>>>(Grid);
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
