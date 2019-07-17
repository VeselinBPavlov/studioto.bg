namespace Studio.Presentation.Tests.ApiControllers.Queries
{    
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common;
    using Studio.Application.Addresses.Queries.GetAllNames;
    using Studio.Application.Cities.Queries.GetAllNames;
    using Studio.Application.Clients.Queries.GetAllNames;
    using Studio.Application.Employees.Queries.GetAllNames;
    using Studio.Application.Industries.Queries.GetAllNames;
    using Studio.Application.Locations.Queries.GetAllNames;
    using Studio.Application.Services.Queries.GetAllNames;
    using Studio.User.WebApp;
    using Xunit;

    public class GetAllNames : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public GetAllNames(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }        

        [Fact]
        public async Task ReturnAllCitiesNames()
        {
            var response = await client.GetAsync("/api/Cities/GetAllNames");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<CitiesNamesListViewModel>(response);

            Assert.IsType<CitiesNamesListViewModel>(vm);
            Assert.NotEmpty(vm.Cities);
        }

        [Fact]
        public async Task ReturnAllAddressesNames()
        {
            var response = await client.GetAsync("/api/Addresses/GetAllNames");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<AddressesNamesListViewModel>(response);

            Assert.IsType<AddressesNamesListViewModel>(vm);
            Assert.NotEmpty(vm.Addresses);
        }

        [Fact]
        public async Task ReturnAllClientsNames()
        {
            var response = await client.GetAsync("/api/Clients/GetAllNames");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<ClientsNamesListViewModel>(response);

            Assert.IsType<ClientsNamesListViewModel>(vm);
            Assert.NotEmpty(vm.Clients);
        }

        [Fact]
        public async Task ReturnAllEmployeesNames()
        {
            var response = await client.GetAsync("/api/Employees/GetAllNames");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<EmployeesNamesListViewModel>(response);

            Assert.IsType<EmployeesNamesListViewModel>(vm);
            Assert.NotEmpty(vm.Employees);
        }

        [Fact]
        public async Task ReturnAllIndustriesNames()
        {
            var response = await client.GetAsync("/api/Industries/GetAllNames");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<IndustriesNamesListViewModel>(response);

            Assert.IsType<IndustriesNamesListViewModel>(vm);
            Assert.NotEmpty(vm.Industries);
        }

        [Fact]
        public async Task ReturnAllLocationsNames()
        {
            var response = await client.GetAsync("/api/Locations/GetAllNames");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<LocationsNamesListViewModel>(response);

            Assert.IsType<LocationsNamesListViewModel>(vm);
            Assert.NotEmpty(vm.Locations);
        }

        [Fact]
        public async Task ReturnAllServicesNames()
        {
            var response = await client.GetAsync("/api/Services/GetAllNames");

           response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<ServicesNamesListViewModel>(response);

            Assert.IsType<ServicesNamesListViewModel>(vm);
            Assert.NotEmpty(vm.Services);
        }        
    }
}
