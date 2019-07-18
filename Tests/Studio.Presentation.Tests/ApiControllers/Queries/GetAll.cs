namespace Studio.Presentation.Tests.ApiControllers.Queries
{    
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;
    using Studio.Application.Addresses.Queries.GetAllAddresses;
    using Studio.Application.Cities.Queries.GetAllCities;
    using Studio.Application.Clients.Queries.GetAllClients;
    using Studio.Application.Countries.Queries.GetAllCountries;
    using Studio.Application.Employees.Queries.GetAllEmployees;
    using Studio.Application.EmployeeServices.Queries.GetAllEmployeeServices;
    using Studio.Application.Industries.Queries.GetAllIndustries;
    using Studio.Application.LocationIndustries.Queries.GetAllLocationIndustries;
    using Studio.Application.Locations.Queries.GetAllLocations;
    using Studio.Application.Services.Queries.GetAllServices;
    using Studio.User.WebApp;
    using Studio.User.WebApp.Areas.Administrator.api;
    using Xunit;

    public class GetAll : IClassFixture<CustomWebApplicationFactory<Startup>>
    {        
        [Fact]
        public void GetAllCountriesShouldReturnCountriesListViewModel()
            => MyController<CountriesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<CountriesListViewModel>>();

        [Fact]
        public void GetAllCitiesShouldReturnCitiesListViewModel()
            => MyController<CitiesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<CitiesListViewModel>>();

        [Fact]
        public void GetAllAddressesShouldReturnAddressesListViewModel()
            => MyController<AddressesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<AddressesListViewModel>>();

        [Fact]
        public void GetAllClientsShouldReturnClientsListViewModel()
            => MyController<ClientsController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<ClientsListViewModel>>();

        [Fact]
        public void GetAllLocationsShouldReturnLocationsListViewModel()
            => MyController<LocationsController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<LocationsListViewModel>>();
        
        [Fact]
        public void GetAllIndustriesShouldReturnIndustriesListViewModel()
            => MyController<IndustriesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<IndustriesListViewModel>>();

        [Fact]
        public void GetAllEmployeesShouldReturnEmployeesListViewModel()
            => MyController<EmployeesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<EmployeesListViewModel>>();

        [Fact]
        public void GetAllServicesShouldReturnServicesListViewModel()
            => MyController<ServicesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<ServicesListViewModel>>();

        [Fact]
        public void GetAllServicesShouldReturnLocationIndustriesListViewModel()
            => MyController<LocationIndustriesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<LocationIndustriesListViewModel>>();

        [Fact]
        public void GetAllServicesShouldReturnEmployeeServicesListViewModel()
            => MyController<EmployeeServicesController>
                .Instance()
                .Calling(c => c.GetAll())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ActionResult<EmployeeServicesListViewModel>>();
    }
}
