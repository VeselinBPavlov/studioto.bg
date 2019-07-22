namespace Studio.User.WebApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Appointments.Commands.Create;
    using Studio.Application.Employees.Queries.GetPageEmployeeById;
    using Studio.User.WebApp.Models;

    public class EmployeeController : BaseController
    {
        public async Task<IActionResult> Workspace(int id)
        {
            var employee = await Mediator.Send(new GetEmployeeProfileByIdQuery { Id = id });
            var command = new CreateAppointmentCommand();
            var employeeAppointmentDto = new EmployeeAppointmentDto 
            { 
                EmployeeProfileViewModel = employee, 
                CreateAppointmentCommand = command 
            };
            return this.View(employeeAppointmentDto);
        }
    }
}