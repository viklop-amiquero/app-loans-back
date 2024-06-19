using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Puesto_.Commands.NewPuesto
{
    public class NewPuestoHandler : IRequestHandler<NewPuestoVM, Iresult>
    {
        private readonly IRepository<Puesto> _repositoryPuesto;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewPuestoHandler(
            IRepository<Puesto> puestoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor
            )
        {
            _repositoryPuesto = puestoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }
        public async Task<Iresult> Handle(NewPuestoVM request, CancellationToken cancellationToken)
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

            request.V_NAME = request.V_NAME.ToUpper();
            var datos = _repositoryPuesto.TableNoTracking.Where(x => x.V_NOMBRE == request.V_NAME).ToList().Count == 0;
            if (_repositoryPuesto.TableNoTracking.Where(x => x.V_NOMBRE == request.V_NAME).ToList().Count == 0)
            {

                _repositoryPuesto.Insert(new List<Puesto>
                {
                    new Puesto
                    {
                        V_NOMBRE = request.V_NAME,
                        V_DESCRIPCION = request.V_DESCRIPTION == "" ? null : request.V_DESCRIPTION,
                        B_ESTADO = "1",
                        I_USUARIO_CREACION =usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now
                    }
                });
                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Registro ya existente (nombre) o registro inactivo")
                }
            };
        }
    }
}