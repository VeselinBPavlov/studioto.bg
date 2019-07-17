namespace Studio.Presentation.Tests.ApiControllers.Commands
{
    using System.Net.Http;
    using Studio.Presentation.Tests.Common;
    using Studio.User.WebApp;
    using Xunit;

    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public Create(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        // [Fact]
        // public async Task CreateCountryCommandReturnsSuccessStatusCode()
        // {
        //     var command = new CreateCountryCommand
        //     {
        //         Name = "Bangladesh"
        //     };

        //     var content = Utilities.GetRequestContent(command);

        //     var response = await this.client.PostAsync($"/api/Countries/Create", content);

        //     response.EnsureSuccessStatusCode();
        // }
    }
}