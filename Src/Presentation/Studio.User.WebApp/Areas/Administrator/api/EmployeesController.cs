namespace Studio.User.WebApp.Areas.Administrator.api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Employees.Commands.Create;
    using Studio.Application.Employees.Commands.Delete;
    using Studio.Application.Employees.Commands.Update;
    using Studio.Application.Employees.Queries.GetAllEmployees;
    using Studio.Application.Employees.Queries.GetAllNames;
    using Studio.Application.Employees.Queries.GetEmployeeById;

    public class EmployeesController : BaseApiController
    {
        // GET: api/Employees/GetAll
        [HttpGet]
        public async Task<ActionResult<EmployeesListViewModel>> GetAll()
        {
            var result = await Mediator.Send(new GetAllEmployeesListQuery());
            return Ok(result);
        }

        // GET: api/Employees/GetAllNames
        [HttpGet]
        public async Task<ActionResult<EmployeesNamesListViewModel>> GetAllNames()
        {
            var result = await Mediator.Send(new GetEmployeesNamesListQuery());
            return Ok(result);
        }

        // GET: api/Employees/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> Get(int id)
        {
            var result = await Mediator.Send(new GetEmployeeByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/Employees/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateEmployeeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Employees/Update/5
        [HttpPut]
        public async Task<ActionResult> Update([FromForm]UpdateEmployeeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }        

        // DELETE: api/Employees/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmployeeCommand { Id = id });

            return NoContent();
        }
    }
}