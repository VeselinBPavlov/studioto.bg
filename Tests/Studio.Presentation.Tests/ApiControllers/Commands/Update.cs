using System.Net.Http;
using System.Threading.Tasks;
using Studio.Application.Countries.Commands.Update;
using Studio.Common;
using Studio.Presentation.Tests.Common;
using Studio.User.WebApp;
using Xunit;

namespace Studio.Presentation.Tests.ApiControllers.Commands
{
 public class Update : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public Update(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        // [Fact]
        // public async Task UpdateCountryCommandReturnsSuccessStatusCode()
        // {
        //     var command = new UpdateCountryCommand
        //     {
        //         Id = GConst.ValidId,
        //         Name = "Russia"
        //     };

        //     var content = Utilities.GetRequestContent(command);

        //     var response = await client.PutAsync($"/api/Countries/Update", content);

        //     response.EnsureSuccessStatusCode();
        // }
    }
}