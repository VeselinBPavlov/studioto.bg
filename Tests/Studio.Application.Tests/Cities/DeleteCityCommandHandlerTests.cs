namespace Studio.Application.Tests.Cities.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Cities.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteCityCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteCity()
        {
            var city = new City { Name = GlobalConstants.CityValidName };

            context.Cities.Add(city);
            await context.SaveChangesAsync();

            var cityId = context.Cities.SingleOrDefault(x => x.Name == GlobalConstants.CityValidName).Id;

            var sut = new DeleteCityCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteCityCommand { Id = cityId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task CityShouldТhrowDeleteFailureException()
        {
            var city = new City { Name = "Beauty" };

            context.Cities.Add(city);
            await context.SaveChangesAsync();

            var cityId = context.Cities.SingleOrDefault(x => x.Name == "Beauty").Id;

            var address = new Address { Longitude = 40.00M, CityId = cityId };
            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var sut = new DeleteCityCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCityCommand { Id = cityId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.CityDeleteFalueExceptionMessage, cityId), status.Message);
        }

        [Fact]
        public async Task CityShouldТhrowNotFoundException()
        {
            var sut = new DeleteCityCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCityCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.CityNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);
        }
    }
}
