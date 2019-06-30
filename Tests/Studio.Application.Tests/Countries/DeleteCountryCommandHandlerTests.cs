namespace Studio.Application.Tests.Countries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Countries.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteCountryCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteCountry()
        {
            var country = new Country { Name = GConst.CountryValidName };

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var countryId = context.Countries.SingleOrDefault(x => x.Name == GConst.CountryValidName).Id;

            var sut = new DeleteCountryCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteCountryCommand { Id = countryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task CountryShouldТhrowDeleteFailureException()
        {
            var country = new Country { Name = "Beauty" };

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var countryId = context.Countries.SingleOrDefault(x => x.Name == "Beauty").Id;

            var city = new City { Name = GConst.CityValidName, CountryId = countryId };
            context.Cities.Add(city);
            await context.SaveChangesAsync();

            var sut = new DeleteCountryCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCountryCommand { Id = countryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, nameof(Country), countryId, "cities", "country"), status.Message);
        }
        

        [Fact]
        public async Task CountryShouldТhrowNotFoundException()
        {
            var sut = new DeleteCountryCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCountryCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, nameof(Country), GConst.InvalidId), status.Message);
        }
    }
}
