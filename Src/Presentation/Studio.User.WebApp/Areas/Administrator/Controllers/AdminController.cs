namespace Studio.User.WebApp.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
    }
}