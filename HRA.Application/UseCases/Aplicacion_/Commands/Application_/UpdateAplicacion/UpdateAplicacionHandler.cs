using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.UpdateAplicacion
{
    public class UpdateAplicacionHandler : IRequestHandler<UpdateAplicacionVM, Iresult>
    {
        private readonly IRepository<Aplicacion> _repositoryAplicacion;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAplicacionHandler(
            IRepository<Aplicacion> aplicacionRepository,
            IRepository<Usuario> repositoryUsuario,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryAplicacion = aplicacionRepository;
            _repositoryUsuario = repositoryUsuario;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateAplicacionVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryAplicacion.Table.FirstOrDefault(x => x.I_ID_APLICACION == request.I_APLICATION_ID && x.B_ESTADO == "1");

            if (entity == null) {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe la aplicación o está inactiva")
                    }
                };

            }

            request.V_APLICATION = request.V_APLICATION.ToUpper();
            if (_repositoryAplicacion.TableNoTracking.Where(x => (x.V_APLICACION == request.V_APLICATION || x.V_ACRONIMO == request.V_ACRONYM.ToUpper()) 
                    && x.I_ID_APLICACION != request.I_APLICATION_ID).ToList().Count == 0)
            {
                entity.V_APLICACION = request.V_APLICATION == "" ? entity.V_APLICACION: request.V_APLICATION;
                entity.V_ACRONIMO = request.V_ACRONYM == "" ? entity.V_ACRONIMO : request.V_ACRONYM == "null" ? null : request.V_ACRONYM.ToUpper();
                entity.V_DESCRIPCION = request.V_DESCRIPTION == "" ? entity.V_DESCRIPCION : request.V_DESCRIPTION == "null" ? null : request.V_DESCRIPTION;
                entity.V_URL = request.V_URL == "" ? entity.V_URL : request.V_URL == "null" ? null : request.V_URL;
                entity.B_ESTADO = "1";
                entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                entity.D_FECHA_MODIFICA = _repositoryDate.Now;

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                     new DetailError("06", "Registro ya existente (nombre o acrónimo)")
                }
            };
        }

    }
}
