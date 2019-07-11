namespace Studio.User.WebApp.Areas.Administrator.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Countries.Commands.Create;
    using Studio.Application.Countries.Commands.Delete;
    using Studio.Application.Countries.Commands.Update;
    using Studio.Application.Countries.Queries.GetAllCountries;
    using Studio.Application.Countries.Queries.GetCountryById;

    public class CountriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CountriesListViewModel>> GetAll() 
            => View(await Mediator.Send(new GetAllCountriesListQuery()));
        

        [HttpGet]
        public async Task<ActionResult<CountryViewModel>> Get(int id) 
            => View(await Mediator.Send(new GetCountryByIdQuery { Id = id }));
        

        [HttpPost]
        public async Task<IActionResult> Create(CreateCountryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateCountryCommand command)
        {
            await Mediator.Send(command);

            return this.RedirectToAction();
        }        

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCountryCommand { Id = id });

            return this.RedirectToAction();
        }
    }
}