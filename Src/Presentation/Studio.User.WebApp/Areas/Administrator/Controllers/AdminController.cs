namespace Studio.User.WebApp.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Countries()
        {
            return View();
        }

        public IActionResult Cities()
        {
            return View();
        }

        public IActionResult Addresses() 
        {
            return this.View();
        }

        public IActionResult Employees() 
        {
            return this.View();
        }

        public IActionResult Clients()
        {
            return this.View();
        }

        public IActionResult Locations()
        {
            return this.View();
        }

        public IActionResult Industries()
        {
            return this.View();
        }

        public IActionResult Services()
        {
            return this.View();
        }

        public IActionResult EmployeeServices()
        {
            return this.View();
        }

        public IActionResult LocationIndustries()
        {
            return this.View();
        }
    }
}