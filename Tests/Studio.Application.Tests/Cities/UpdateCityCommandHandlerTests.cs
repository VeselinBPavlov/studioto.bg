namespace Studio.Application.Tests.Cities.Commands
{
    using MediatR;
    using Studio.Application.Cities.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateCityCommandHandlerTests : CommandTestBase
    {   
        [Fact]
        public async void CityShouldUpdateCorrect()
        {
            var country = new Country { Name = GConst.CountryValidName };

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var countryId = context.Countries.SingleOrDefault(x => x.Name == GConst.CountryValidName).Id;

            var city = new City { Name = GConst.CityValidName, CountryId = countryId };

            context.Cities.Add(city);
            await context.SaveChangesAsync();

            var cityId = context.Cities.SingleOrDefault(x => x.Name == GConst.CityValidName).Id;

            var sut = new UpdateCityCommandHandler(context);
            var updatedCity = new UpdateCityCommand { Id = cityId, Name = "Mars", CountryId = countryId };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedCity, CancellationToken.None));

            var resultId = context.Cities.SingleOrDefault(x => x.Name == "Mars").Id;

            Assert.Equal(cityId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Cities.Count());
        }

        [Fact]
        public async void CityShouldThrowReferenceException()
        {
            var country = new Country { Name = GConst.CountryValidName };

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var countryId = context.Countries.SingleOrDefault(x => x.Name == GConst.CountryValidName).Id;

            var city = new City { Name = GConst.CityValidName, CountryId = countryId };

            context.Cities.Add(city);
            await context.SaveChangesAsync();

            var cityId = context.Cities.SingleOrDefault(x => x.Name == GConst.CityValidName).Id;

            var sut = new UpdateCityCommandHandler(context);
            var updatedCity = new UpdateCityCommand { Id = cityId, Name = "Mars", CountryId = GConst.InvalidId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedCity, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, "Update", GConst.City, cityId, GConst.CountryLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void CityShouldThrowNotFoundException()
        {
            var sut = new UpdateCityCommandHandler(context);
            var updatedCity = new UpdateCityCommand { Id = GConst.InvalidId, Name = GConst.CityValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedCity, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, nameof(City), GConst.InvalidId), status.Message);            
        }
    }
}
