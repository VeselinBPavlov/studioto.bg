using System.Net.Http;
using System.Threading.Tasks;
using Studio.Common;
using Studio.Presentation.Tests.Common;
using Studio.User.WebApp;
using Xunit;

namespace Studio.Presentation.Tests.ApiControllers.Commands
{
    public class Delete : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public Delete(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        // [Fact]
        // public async Task GivenIdReturnsSuccessStatusCode()
        // {
        //     var id = GConst.ValidId;

        //     var response = await this.client.DeleteAsync($"/api/Countries/Delete/{id}");

        //     response.EnsureSuccessStatusCode();
        // }
    }
}