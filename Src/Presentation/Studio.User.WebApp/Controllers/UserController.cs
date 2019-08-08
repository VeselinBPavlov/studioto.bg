namespace Studio.User.WebApp.Controllers
{
    using System.Threading.Tasks;
    using Application.Appointments.Queries.GetAppointmentsByUserId;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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