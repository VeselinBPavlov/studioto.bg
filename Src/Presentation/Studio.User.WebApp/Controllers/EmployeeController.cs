namespace Studio.User.WebApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Employees.Queries.GetPageEmployeeById;

    public class EmployeeController : BaseController
    {
        public async Task<IActionResult> Workspace(int id)
        {
            var result = await Mediator.Send(new GetEmployeeProfileByIdQuery { Id = id });
            return this.View(result);
        }
    }
}