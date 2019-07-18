namespace Studio.User.WebApp.Areas.Administrator.api
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator => this.mediator ?? (this.mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}