namespace Studio.User.WebApp.Areas.Administrator.api
{
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Locations.Commands.Create;
    using Studio.Application.Locations.Commands.Delete;
    using Studio.Application.Locations.Commands.Update;
    using Studio.Application.Locations.Commands.UploadFile;
    using Studio.Application.Locations.Queries.GetAllLocations;
    using Studio.Application.Locations.Queries.GetAllLocationsNames;
    using Studio.Application.Locations.Queries.GetLocationById;
    using System.Threading.Tasks;

    public class LocationsController : BaseApiController
    {
        // GET: api/Locations/GetAll
        [HttpGet]
        public async Task<ActionResult<LocationsListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllLocationsListQuery());
            return Ok(result);
        }

        // GET: api/Locations/GetAllNames
        [HttpGet]
        public async Task<ActionResult<LocationsNamesListViewModel>> GetAllNames()
        {
            var result = await Mediator.Send(new GetLocationsNamesListQuery());
            return Ok(result);
        }

        // GET: api/Locations/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetLocationByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Locations/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateLocationCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Locations/Update/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateLocationCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Locations/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteLocationCommand { Id = id });

            return NoContent();
        }

        // POST: api/Locations/UploadFile
        [HttpPost]
        public async Task<ActionResult> UploadFile([FromForm]UploadLocationFileCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}