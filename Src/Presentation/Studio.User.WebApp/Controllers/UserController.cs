namespace Studio.User.WebApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Appointments.Queries.GetAppointmentsByUserId;
    using Studio.Common;

    [Authorize(Roles = GConst.UserRole)]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            var result = await Mediator.Send(new GetAppointmentsByUserIdListQuery { UserId = id });
            return this.View(result);
        }
    }
}