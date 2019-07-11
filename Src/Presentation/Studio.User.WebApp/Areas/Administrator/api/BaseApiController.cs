namespace Studio.User.WebApp.Areas.Administrator.api
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator => this.mediator ?? (this.mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}