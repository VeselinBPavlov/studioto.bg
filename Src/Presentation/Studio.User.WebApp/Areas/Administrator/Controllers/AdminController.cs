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
    }
}