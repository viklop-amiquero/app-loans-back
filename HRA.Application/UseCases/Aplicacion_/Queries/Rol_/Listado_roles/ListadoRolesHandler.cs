using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure.SP_Rol;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Listado_roles
{
    public class ListadoRolesHandler : IRequestHandler<ListadoRolesVM,Iresult>
    {
        private readonly IRepository<Rol> _repositoryRol;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListadoRolesHandler (
            IRepository<Rol> rolRepository,
            IRepository<Usuario> usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositoryRol = rolRepository;
            _repositoryUsuario = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(ListadoRolesVM request, CancellationToken cancellationToken)
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

            var sp_listado_roles = await _unitOfWork.ExcuteStoreQueryList<entity_Listado_rol>("[seguridad].[USP_SEL_ROL] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoRolesDTO>>(sp_listado_roles.Item1);
            var Grid = new ResultGrid<List<ListadoRolesDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_roles.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoRolesDTO>>>(Grid);
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
