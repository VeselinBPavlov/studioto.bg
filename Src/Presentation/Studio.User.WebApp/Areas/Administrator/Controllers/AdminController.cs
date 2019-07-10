namespace Studio.User.WebApp.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}