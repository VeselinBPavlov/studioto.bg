namespace Studio.Presentation.Tests.ApiControllers.Queries
{    
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;
    using Studio.Application.Addresses.Queries.GetAllNames;
    using Studio.Application.Cities.Queries.GetAllNames;
    using Studio.Application.Clients.Queries.GetAllNames;
    using Studio.Application.Employees.Queries.GetAllNames;
    using Studio.Application.Industries.Queries.GetAllNames;
    using Studio.Application.Locations.Queries.GetAllNames;
    using Studio.Application.Services.Queries.GetAllNames;
    using Studio.User.WebApp;
    using Studio.User.WebApp.Areas.Administrator.api;
    using Xunit;

    public class GetAllNames : IClassFixture<CustomWebApplicationFactory<Startup>>
    {      
        [Fact]
        public void GetAllNamesCitiesShouldReturnCitiesNamesListViewModel()
            => MyController<CitiesController>
                .Instance()
                .Calling(c => c.GetAllNames())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<CitiesNamesListViewModel>>();

        [Fact]
        public void GetAllNamesAddressesShouldReturnAddressesNamesListViewModel()
            => MyController<AddressesController>
                .Instance()
                .Calling(c => c.GetAllNames())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<AddressesNamesListViewModel>>();

        [Fact]
        public void GetAllNamesClientsShouldReturnClientsNamesListViewModel()
            => MyController<ClientsController>
                .Instance()
                .Calling(c => c.GetAllNames())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<ClientsNamesListViewModel>>();

        [Fact]
        public void GetAllNamesLocationsShouldReturnLocationsNamesListViewModel()
            => MyController<LocationsController>
                .Instance()
                .Calling(c => c.GetAllNames())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<LocationsNamesListViewModel>>();
        
        [Fact]
        public void GetAllNamesIndustriesShouldReturnIndustriesNamesListViewModel()
            => MyController<IndustriesController>
                .Instance()
                .Calling(c => c.GetAllNames())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<IndustriesNamesListViewModel>>();

        [Fact]
        public void GetAllNamesEmployeesShouldReturnEmployeesNamesListViewModel()
            => MyController<EmployeesController>
                .Instance()
                .Calling(c => c.GetAllNames())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<EmployeesNamesListViewModel>>();

        [Fact]
        public void GetAllNamesServicesShouldReturnServicesNamesListViewModel()
            => MyController<ServicesController>
                .Instance()
                .Calling(c => c.GetAllNames())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<ServicesNamesListViewModel>>();
    }
}
