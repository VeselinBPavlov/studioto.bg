namespace Studio.User.WebApp.Areas.Administrator.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [Area("Administrator")]
    public abstract class BaseController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator => this.mediator ?? (this.mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}