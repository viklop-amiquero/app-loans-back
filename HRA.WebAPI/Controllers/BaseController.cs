using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : Controller
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
