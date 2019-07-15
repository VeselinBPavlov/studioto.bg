namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Cities.Queries.GetLocationIndustryById;
    using Studio.Application.LocationIndustries.Commands.Create;
    using Studio.Application.LocationIndustries.Commands.Delete;
    using Studio.Application.LocationIndustries.Commands.Update;
    using Studio.Application.LocationIndustries.Queries.GetAllLocationIndustries;

    public class LocationIndustriesController : BaseApiController
    {
        // GET: api/LocationIndustries/GetAll
        [HttpGet]
        public async Task<ActionResult<LocationIndustriesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllLocationIndustriesListQuery());
            return Ok(result);
        }

        // GET: api/LocationIndustries/Get/5/5
        [HttpGet("{locationId}/{industryId}")]
        public async Task<ActionResult<LocationIndustryViewModel>> Get(int locationId, int industryId)
        {
            var result = await Mediator.Send(new GetLocationIndustryByIdQuery { LocationId = locationId, IndustryId = industryId });
            return Ok(result);
        }

        // POST: api/LocationIndustries/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateLocationIndustryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/LocationIndustries/Update/5/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateLocationIndustryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/LocationIndustries/Delete/5/5
        [HttpDelete("{locationId}/{industryId}")]
        public async Task<ActionResult> Delete(int locationId, int industryId)
        {
            await Mediator.Send(new DeleteLocationIndustryCommand { LocationId = locationId, IndustryId = industryId });

            return NoContent();
        }
    }
}