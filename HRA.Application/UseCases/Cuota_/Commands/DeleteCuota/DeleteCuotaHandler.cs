﻿using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuota_.Commands.DeleteCuota
{
    public class DeleteCuotaHandler : IRequestHandler<DeleteCuotaVM, Iresult>
    {
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCuotaHandler(
            IRepository<Cuota> cuotaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryCuota= cuotaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(DeleteCuotaVM request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccesor?.HttpContext?.User?.Claims;
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

            var entity = _repositoryCuota.Table.FirstOrDefault(x => x.I_ID_CUOTA == request.I_CUOTA_ID);

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el puesto")
                    }
                };

            }

            entity.B_ESTADO = "0";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;


            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
