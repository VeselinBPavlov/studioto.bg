namespace Studio.User.WebApp.Components
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    public class AppointmentCreateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}