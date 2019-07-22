﻿namespace Studio.User.WebApp.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.ContactForms.Commands.Create;
    using Studio.Application.Locations.Queries.GetFilteredLocations;
    using WebApp.Models;

    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetFilteredLocationsListQuery());
            return this.View(result);
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Steps()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contacts([FromForm]CreateContactFormCommand command)
        {
            await Mediator.Send(command);
            return this.Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
