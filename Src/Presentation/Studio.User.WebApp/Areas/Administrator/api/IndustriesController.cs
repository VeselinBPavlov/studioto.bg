namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Application.Industries.Commands.Delete;
    using Studio.Application.Industries.Commands.Update;
    using Studio.Application.Industries.Queries.GetAllIndustries;
    using Studio.Application.Industries.Queries.GetAllIndustriesNames;
    using Studio.Application.Industries.Queries.GetIndustryById;

    public class IndustriesController : BaseApiController
    {
        // GET: api/Industries/GetAll
        [HttpGet]
        public async Task<ActionResult<IndustriesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllIndustriesListQuery());
            return Ok(result);
        }

        // GET: api/Industries/GetAllNames
        [HttpGet]
        public async Task<ActionResult<IndustriesNamesListViewModel>> GetAllNames()
        {
            var result = await Mediator.Send(new GetIndustriesNamesListQuery());
            return Ok(result);
        }

        // GET: api/Industries/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IndustryViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetIndustryByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Industries/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateIndustryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Industries/Update/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateIndustryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Industries/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteIndustryCommand { Id = id });

            return NoContent();
        }
    }
}