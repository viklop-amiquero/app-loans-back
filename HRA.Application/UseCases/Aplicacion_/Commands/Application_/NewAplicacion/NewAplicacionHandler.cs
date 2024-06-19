using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.NewAplicacion
{
    public class NewAplicacionHandler : IRequestHandler<NewAplicacionVM, Iresult>
    {
        private readonly IRepository<Aplicacion> _repositoryAplicacion;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public NewAplicacionHandler(
            IRepository<Aplicacion> aplicacionRepository, 
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryAplicacion = aplicacionRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewAplicacionVM request, CancellationToken cancellationToken)
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

            request.V_APLICATION = request.V_APLICATION.ToUpper();
            if (_repositoryAplicacion.TableNoTracking.Where(x => x.V_APLICACION == request.V_APLICATION && x.V_ACRONIMO == request.V_ACRONYM.ToUpper()).ToList().Count == 0)
            {
                _repositoryAplicacion.Insert(new List<Aplicacion>
                {
                    new Aplicacion
                    {
                        V_APLICACION = request.V_APLICATION,
                        V_ACRONIMO = request.V_ACRONYM == "" ? null : request.V_ACRONYM == "null" ? null : request.V_ACRONYM.ToUpper(),
                        V_DESCRIPCION = request.V_DESCRIPTION == "" ? null : request.V_DESCRIPTION == "null" ? null : request.V_DESCRIPTION,
                        V_URL = request.V_URL == "" ? null : request.V_URL == "null" ? null : request.V_URL,
                        B_ESTADO = "1",
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now,
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
                     new DetailError("06", "Registro ya existente (nombre o acronimo) o registro inactivo")
                }
            };
        }
    }
}
