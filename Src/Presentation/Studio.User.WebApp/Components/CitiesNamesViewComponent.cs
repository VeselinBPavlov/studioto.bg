namespace Studio.User.WebApp.Components
{
    using System.Threading.Tasks;
    using Application.Cities.Queries.GetAllCitiesNames;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class CitiesNamesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

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