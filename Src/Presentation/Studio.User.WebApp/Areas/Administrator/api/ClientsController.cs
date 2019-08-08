namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Clients.Commands.Create;
    using Studio.Application.Clients.Commands.Delete;
    using Studio.Application.Clients.Commands.Update;
    using Studio.Application.Clients.Queries.GetAllClients;
    using Studio.Application.Clients.Queries.GetAllClientsNames;
    using Studio.Application.Clients.Queries.GetClientById;

    public class ClientsController : BaseApiController
    {
        // GET: api/Clients/GetAll
        [HttpGet]
        public async Task<ActionResult<ClientsListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllClientsListQuery());
            return Ok(result);
        }

        // GET: api/Clients/GetAllNames
        [HttpGet]
        public async Task<ActionResult<ClientsNamesListViewModel>> GetAllNames()
        {
            var result = await Mediator.Send(new GetClientsNamesListQuery());
            return Ok(result);
        }

        // GET: api/Clients/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetClientByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Clients/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateClientCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Clients/Update/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateClientCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Clients/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteClientCommand { Id = id });

            return NoContent();
        }
    }
}