namespace Studio.Presentation.Tests.ApiControllers.Queries
{    
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common;
    using Studio.Application.Addresses.Queries.GetAllAddresses;
    using Studio.Application.Cities.Queries.GetAllCities;
    using Studio.Application.Cities.Queries.GetEmployeesByLocation;
    using Studio.Application.Clients.Queries.GetAllClients;
    using Studio.Application.Countries.Queries.GetAllCountries;
    using Studio.Application.EmployeeServices.Queries.GetAllEmployeeServices;
    using Studio.Application.Industries.Queries.GetAllIndustries;
    using Studio.Application.LocationIndustries.Queries.GetAllLocationIndustries;
    using Studio.Application.Locations.Queries.GetAllLocations;
    using Studio.Application.Services.Queries.GetAllServices;
    using Studio.User.WebApp;
    using Xunit;

    public class GetAll : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public GetAll(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnAllCountries()
        {
            var response = await client.GetAsync("/api/Countries/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<CountriesListViewModel>(response);

            Assert.IsType<CountriesListViewModel>(vm);
            Assert.NotEmpty(vm.Countries);
        }

        [Fact]
        public async Task ReturnAllCities()
        {
            var response = await client.GetAsync("/api/Cities/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<CitiesListViewModel>(response);

            Assert.IsType<CitiesListViewModel>(vm);
            Assert.NotEmpty(vm.Cities);
        }

        [Fact]
        public async Task ReturnAllAddresses()
        {
            var response = await client.GetAsync("/api/Addresses/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<AddressesListViewModel>(response);

            Assert.IsType<AddressesListViewModel>(vm);
            Assert.NotEmpty(vm.Addresses);
        }

        [Fact]
        public async Task ReturnAllClients()
        {
            var response = await client.GetAsync("/api/Clients/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<ClientsListViewModel>(response);

            Assert.IsType<ClientsListViewModel>(vm);
            Assert.NotEmpty(vm.Clients);
        }

        [Fact]
        public async Task ReturnAllEmployees()
        {
            var response = await client.GetAsync("/api/Employees/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<EmployeesListViewModel>(response);

            Assert.IsType<EmployeesListViewModel>(vm);
            Assert.NotEmpty(vm.Employees);
        }

        [Fact]
        public async Task ReturnAllIndustries()
        {
            var response = await client.GetAsync("/api/Industries/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<IndustriesListViewModel>(response);

            Assert.IsType<IndustriesListViewModel>(vm);
            Assert.NotEmpty(vm.Industries);
        }

        [Fact]
        public async Task ReturnAllLocations()
        {
            var response = await client.GetAsync("/api/Locations/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<LocationsListViewModel>(response);

            Assert.IsType<LocationsListViewModel>(vm);
            Assert.NotEmpty(vm.Locations);
        }

        [Fact]
        public async Task ReturnAllServices()
        {
            var response = await client.GetAsync("/api/Services/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<ServicesListViewModel>(response);

            Assert.IsType<ServicesListViewModel>(vm);
            Assert.NotEmpty(vm.Services);
        }

        [Fact]
        public async Task ReturnAllEmployeeServices()
        {
            var response = await client.GetAsync("/api/EmployeeServices/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<EmployeeServicesListViewModel>(response);

            Assert.IsType<EmployeeServicesListViewModel>(vm);
            Assert.NotEmpty(vm.EmployeeServices);
        }

        [Fact]
        public async Task ReturnAllLocationIndustries()
        {
            var response = await client.GetAsync("/api/LocationIndustries/GetAll");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<LocationIndustriesListViewModel>(response);

            Assert.IsType<LocationIndustriesListViewModel>(vm);
            Assert.NotEmpty(vm.LocationIndustries);
        }
    }
}
