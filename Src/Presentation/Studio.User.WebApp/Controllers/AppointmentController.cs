namespace Studio.User.WebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Studio.Application.Appointments.Commands.Create;
    using Studio.Application.Appointments.Queries.GetAvailableAppointments;
    using WebApp.Models;

    public class AppointmentController : BaseController
    {
        [HttpGet]
        public IActionResult Success()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateAppointmentCommand command)
        {               
            await Mediator.Send(command);
            return this.Redirect("/Appointment/Success");
        }

        [HttpPost]
        public async Task<JsonResult> GetAvailableAppointments([FromForm]CreateAppointmentCommand command)
        {
            var result = await Mediator.Send(new GetAvailableAppointmentsQuery { Command = command });

            List<SelectListItem> resultlist = result.AvailableAppointments;
            return Json(resultlist);
        }
    }
}