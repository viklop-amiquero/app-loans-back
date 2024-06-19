using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Interes_ahorro_.Commands.NewInteresAhorro
{
    public class NewTasaInteresHandler : IRequestHandler<NewInteresAhorroVM, Iresult>
    {
        private readonly IRepository<Interes_ahorro> _repositoryInteresAhorro;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewTasaInteresHandler(
            IRepository<Interes_ahorro> interesAhorroRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor
            )
        {
            _repositoryInteresAhorro = interesAhorroRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }
        public async Task<Iresult> Handle(NewInteresAhorroVM request, CancellationToken cancellationToken)
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
           
            if (_repositoryInteresAhorro.TableNoTracking.Where(x => x.V_NOMBRE == request.V_NAME).ToList().Count == 0)
            {

                _repositoryInteresAhorro.Insert(new List<Interes_ahorro>
                {
                    new Interes_ahorro
                    {
                        I_TASA_INTERES = Convert.ToDecimal(request.I_INTEREST),
                        V_NOMBRE = request.V_NAME,
                        V_FRECUENCIA = request.V_FREQUENCY == "" ? null : request.V_FREQUENCY?.ToUpper(),
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
                    new DetailError("06", "El tipo de tasa de interes ya existente o está inactivo")
                }
            };
        }
    }
}