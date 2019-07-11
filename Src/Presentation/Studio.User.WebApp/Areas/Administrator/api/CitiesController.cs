﻿namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Cities.Commands.Create;
    using Studio.Application.Cities.Commands.Delete;
    using Studio.Application.Cities.Commands.Update;
    using Studio.Application.Cities.Queries.GetAllCities;
    using Studio.Application.Cities.Queries.GetCityById;

    public class CitiesController : BaseApiController
    {
        // GET: api/Cities/GetAll
        [HttpGet]
        public async Task<ActionResult<CitiesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllCitiesListQuery());
            return Ok(result);
        }

        // GET: api/Cities/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetCityByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Cities/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCityCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Cities/Update/5
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]UpdateCityCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Cities/Delet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCityCommand { Id = id });

            return NoContent();
        }
    }
}