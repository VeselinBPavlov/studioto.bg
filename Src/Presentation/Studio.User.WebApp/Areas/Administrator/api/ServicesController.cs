namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Services.Commands.Create;
    using Studio.Application.Services.Commands.Delete;
    using Studio.Application.Services.Commands.Update;
    using Studio.Application.Services.Queries.GetAllServices;
    using Studio.Application.Services.Queries.GetAllServicesNames;
    using Studio.Application.Services.Queries.GetServiceById;

    public class ServicesController : BaseApiController
    {
        // GET: api/Services/GetAll
        [HttpGet]
        public async Task<ActionResult<ServicesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllServicesListQuery());
            return Ok(result);
        }

        // GET: api/Services/GetAllNames
        [HttpGet]
        public async Task<ActionResult<ServicesNamesListViewModel>> GetAllNames()
        {
            var result = await Mediator.Send(new GetServicesNamesListQuery());
            return Ok(result);
        }

        // GET: api/Services/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetServiceByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Services/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateServiceCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Services/Update/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateServiceCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Services/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteServiceCommand { Id = id });

            return NoContent();
        }
    }
}