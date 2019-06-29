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
            var city = new City { Name = GlobalConstants.CityValidName };

            context.Cities.Add(city);
            await context.SaveChangesAsync();

            var cityId = context.Cities.SingleOrDefault(x => x.Name == GlobalConstants.CityValidName).Id;

            var sut = new UpdateCityCommandHandler(context);
            var updatedCity = new UpdateCityCommand { Id = cityId, Name = "Mars" };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedCity, CancellationToken.None));

            var resultId = context.Cities.SingleOrDefault(x => x.Name == "Mars").Id;

            Assert.Equal(cityId, resultId);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Cities.Count());
        }

        [Fact]
        public async void CityShouldThrowNotFoundException()
        {
            var sut = new UpdateCityCommandHandler(context);
            var updatedCity = new UpdateCityCommand { Id = GlobalConstants.InvalidId, Name = GlobalConstants.CityValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedCity, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.CityNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);            
        }
    }
}
