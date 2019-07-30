namespace Studio.User.WebApp.Components
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Cities.Queries.GetAllNames;
    using System.Threading.Tasks;

    public class CitiesNamesViewComponent : ViewComponent
    {
        private IMediator mediator;

        public CitiesNamesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await GetAllCitiesNames();

            return View(model.Value);
        }

        [HttpGet]
        public async Task<ActionResult<CitiesNamesListViewModel>> GetAllCitiesNames()
        {
            return await mediator.Send(new GetCitiesNamesListQuery());
        }
    }
}