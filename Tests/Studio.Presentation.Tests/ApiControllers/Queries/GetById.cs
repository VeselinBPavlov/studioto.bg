namespace Studio.Presentation.Tests.ApiControllers.Queries
{    
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common;
    using Studio.Application.Addresses.Queries.GetAddressById;
    using Studio.Application.Cities.Queries.GetCityById;
    using Studio.Application.Cities.Queries.GetEmployeesByLocation;
    using Studio.Application.Cities.Queries.GetEmployeeServiceById;
    using Studio.Application.Cities.Queries.GetLocationIndustryById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Countries.Queries.GetCountryById;
    using Studio.Application.Industries.Queries.GetIndustryById;
    using Studio.Application.Locations.Queries.GetLocationById;
    using Studio.Application.Services.Queries.GetServiceById;
    using Studio.Common;
    using Studio.User.WebApp;
    using Xunit;

    public class GetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public GetById(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnCountryById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Countries/Get/{id}");

            response.EnsureSuccessStatusCode();

            var country = await Utilities.GetResponseContent<CountryViewModel>(response);

            Assert.Equal(id, country.Id);
        }   

        [Fact]
        public async Task ReturnCityById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Cities/Get/{id}");

            response.EnsureSuccessStatusCode();

            var city = await Utilities.GetResponseContent<CityViewModel>(response);

            Assert.Equal(id, city.Id);
        }     

        [Fact]
        public async Task ReturnAddressById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Addresses/Get/{id}");

            response.EnsureSuccessStatusCode();

            var address = await Utilities.GetResponseContent<AddressViewModel>(response);

            Assert.Equal(id, address.Id);
        }

        [Fact]
        public async Task ReturnClientById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Clients/Get/{id}");

            response.EnsureSuccessStatusCode();

            var company = await Utilities.GetResponseContent<ClientViewModel>(response);

            Assert.Equal(id, company.Id);
        }

        [Fact]
        public async Task ReturnLocationById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Locations/Get/{id}");

            response.EnsureSuccessStatusCode();

            var location = await Utilities.GetResponseContent<LocationViewModel>(response);

            Assert.Equal(id, location.Id);
        }

        [Fact]
        public async Task ReturnIndustryById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Industries/Get/{id}");

            response.EnsureSuccessStatusCode();

            var industry = await Utilities.GetResponseContent<IndustryViewModel>(response);

            Assert.Equal(id, industry.Id);
        }

        [Fact]
        public async Task ReturnEmployeeById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Employees/Get/{id}");

            response.EnsureSuccessStatusCode();

            var employee = await Utilities.GetResponseContent<EmployeeViewModel>(response);

            Assert.Equal(id, employee.Id);
        }

        [Fact]
        public async Task ReturnServiceById()
        {
            var id = GConst.ValidId;

            var response = await client.GetAsync($"/api/Services/Get/{id}");

            response.EnsureSuccessStatusCode();

            var service = await Utilities.GetResponseContent<ServiceViewModel>(response);

            Assert.Equal(id, service.Id);
        }

        [Fact]
        public async Task ReturnEmployeeServiceById()
        {
            var employeeId = GConst.ValidId;
            var serviceId = GConst.ValidId;

            var response = await client.GetAsync($"/api/EmployeeServices/Get/{employeeId}/{serviceId}");

            response.EnsureSuccessStatusCode();

            var employeeService = await Utilities.GetResponseContent<EmployeeServiceViewModel>(response);

            Assert.Equal(employeeId, employeeService.EmployeeId);
            Assert.Equal(serviceId, employeeService.ServiceId);
        }

        [Fact]
        public async Task ReturnLocationIndustryById()
        {
            var locationId = GConst.ValidId;
            var industryId = GConst.ValidId;

            var response = await client.GetAsync($"/api/LocationIndustries/Get/{locationId}/{industryId}");

            response.EnsureSuccessStatusCode();

            var locationIndustry = await Utilities.GetResponseContent<LocationIndustryViewModel>(response);

            Assert.Equal(locationId, locationIndustry.LocationId);
            Assert.Equal(industryId, locationIndustry.IndustryId);
        }
    }
}
