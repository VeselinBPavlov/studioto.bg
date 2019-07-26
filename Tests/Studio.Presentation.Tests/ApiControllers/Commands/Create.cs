namespace Studio.Presentation.Tests.ApiControllers.Commands
{
    using System.Net.Http;
    using MyTested.AspNetCore.Mvc;
    using Studio.Application.Countries.Commands.Create;
    using Studio.Common;
    using Studio.Presentation.Tests.Common;
    using Studio.User.WebApp;
    using Studio.User.WebApp.Areas.Administrator.api;
    using Xunit;

    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
         [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnNoContent()
            => MyController<CountriesController>
                .Instance()
                .Calling(c => c.Create(new CreateCountryCommand { Name = "Bulgaria" }))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(MyTested.AspNetCore.Mvc.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .NoContent();
    }
}