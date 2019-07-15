namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Countries.Commands.Create;
    using Studio.Application.Countries.Commands.Delete;
    using Studio.Application.Countries.Commands.Update;
    using Studio.Application.Countries.Queries.GetAllCountries;
    using Studio.Application.Countries.Queries.GetCountryById;

    public class CountriesController : BaseApiController
    {
        // GET: api/Countries/GetAll
        [HttpGet]
        public async Task<ActionResult<CountriesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllCountriesListQuery());
            return Ok(result);
        }

        // GET: api/Countries/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetCountryByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Countries/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCountryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Countries/Update/5
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]UpdateCountryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Countries/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCountryCommand { Id = id });

            return NoContent();
        }
    }
}