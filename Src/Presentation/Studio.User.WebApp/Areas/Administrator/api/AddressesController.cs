namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Addresses.Commands.Create;
    using Studio.Application.Addresses.Commands.Delete;
    using Studio.Application.Addresses.Commands.Update;
    using Studio.Application.Addresses.Queries.GetAddressById;
    using Studio.Application.Addresses.Queries.GetAllAddresses;
    using Studio.Application.Addresses.Queries.GetAllNames;

    public class AddressesController : BaseApiController
    {
        // GET: api/Addresses/GetAll
        [HttpGet]
        public async Task<ActionResult<AddressesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllAddressesListQuery());
            return Ok(result);
        }

        // GET: api/Addresses/GetAllNames
        [HttpGet]
        public async Task<ActionResult<AddressesNamesListViewModel>> GetAllNames()
        {
            var result = await Mediator.Send(new GetAddressesNamesListQuery());
            return Ok(result);
        }

        // GET: api/Addresses/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetAddressByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Addresses/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateAddressCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Addresses/Update/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateAddressCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Addresses/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteAddressCommand { Id = id });

            return NoContent();
        }
    }
}