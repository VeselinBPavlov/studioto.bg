namespace Studio.User.WebApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Employees.Queries.GetPageEmployeeById;

    public class ProfileController : BaseController
    {
        public async Task<IActionResult> Details(int id)
        {
            var result = await this.Mediator.Send(new GetEmployeeProfileByIdQuery { Id = id });
            return this.View(result);
        }
    }
}