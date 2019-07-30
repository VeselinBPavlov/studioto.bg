namespace Studio.User.WebApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Studio.Application.Locations.Queries.GetFilteredLocations;
    using Studio.Application.Locations.Queries.GetLocationByIdPage;

    public class LocationController : BaseController
    {
        public async Task<IActionResult> Studio(int id)
        {
            var result = await Mediator.Send(new GetLocationByIdPageQuery { Id = id });
            return this.View(result);
        }

        public async Task<IActionResult> All(int? cityId, string studioName, string serviceName )
        {
            var result = await Mediator.Send(new GetFilteredLocationsListQuery { CityId = cityId, StudioName = studioName, ServiceName = serviceName, IsHomePage = false });
            return this.View(result);
        }
    }
}