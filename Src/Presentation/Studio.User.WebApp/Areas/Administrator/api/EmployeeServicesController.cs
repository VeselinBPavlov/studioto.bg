namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Cities.Queries.GetEmployeeServiceById;
    using Studio.Application.EmployeeServices.Commands.Create;
    using Studio.Application.EmployeeServices.Commands.Delete;
    using Studio.Application.EmployeeServices.Commands.Update;
    using Studio.Application.EmployeeServices.Queries.GetAllEmployeeServices;

    public class EmployeeServicesController : BaseApiController
    {
        // GET: api/EmployeeServices/GetAll
        [HttpGet]
        public async Task<ActionResult<EmployeeServicesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllEmployeeServicesListQuery());
            return Ok(result);
        }

        // GET: api/EmployeeServices/Get/5/5
        [HttpGet("{employeeId}/{serviceId}")]
        public async Task<ActionResult<EmployeeServiceViewModel>> Get(int employeeId, int serviceId)
        {
            var result = await Mediator.Send(new GetEmployeeServiceByIdQuery { EmployeeId = employeeId, ServiceId = serviceId });
            return Ok(result);
        }

        // POST: api/EmployeeServices/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateEmployeeServiceCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/EmployeeServices/Update/5/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateEmployeeServiceCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/EmployeeServices/Delete/5/5
        [HttpDelete("{employeeId}/{serviceId}")]
        public async Task<ActionResult> Delete(int employeeId, int serviceId)
        {
            await Mediator.Send(new DeleteEmployeeServiceCommand { EmployeeId = employeeId, ServiceId = serviceId });

            return NoContent();
        }
    }
}