namespace Studio.Presentation.Tests.ApiControllers.Countries.Commands
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Application.Countries.Commands.Create;
    using Common;
    using Studio.User.WebApp;
    using Xunit;

    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public Create(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenCreateCustomerCommandReturnsSuccessStatusCode()
        {
            var command = new CreateCountryCommand
            {
                Name = "Bulgaria"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await this.client.PostAsync($"/api/Customers/Create", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
