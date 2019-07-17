namespace Studio.User.WebApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Appointments.Commands.Create;
    using WebApp.Models;

    public class AppointmentController : BaseController
    {
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateAppointmentCommand command)
        {               
            await Mediator.Send(command);
            return this.Redirect("/");
        }
    }
}