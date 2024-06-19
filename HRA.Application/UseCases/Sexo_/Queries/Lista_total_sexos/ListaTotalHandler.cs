﻿using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Sexo_.Queries.Lista_total_sexos
{
    public class ListaTotalHandler : IRequestHandler<SexosVM, Iresult>
    {
        private readonly IRepository<Sexo> _repositorySexo;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaTotalHandler(
            IRepository<Sexo> sexoRepository, 
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositorySexo = sexoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(SexosVM request, CancellationToken cancellationToken)
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

            var sexo = _repositorySexo.TableNoTracking.ToList();
            var map = _mapper.Map<List<SexoDTO>>(sexo);

            if (map != null)
            {
                return new SuccessResult<List<SexoDTO>>(map);
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
