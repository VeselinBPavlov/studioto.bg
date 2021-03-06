﻿namespace Studio.User.WebApp.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Application.ContactForms.Commands.Create;
    using Application.Locations.Queries.GetFilteredLocations;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Models;

    public class HomeController : BaseController
    {

        [HttpGet]
        [Route("/")]
        [Route("Home/Index")]
        [Route("/appointment/create")]
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
