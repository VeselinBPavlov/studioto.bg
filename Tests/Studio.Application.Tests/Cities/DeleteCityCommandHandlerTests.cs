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
            var cityId = GetCityId(null);

            var sut = new DeleteCityCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteCityCommand { Id = cityId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task CityShouldТhrowDeleteFailureException()
        {
            var cityId = GetCityId(null);

            GetAddressId(cityId);

            var sut = new DeleteCityCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCityCommand { Id = cityId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.City, cityId, GConst.Addresses, GConst.CityLower), status.Message);
        }

        [Fact]
        public async Task CityShouldТhrowNotFoundException()
        {
            var sut = new DeleteCityCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCityCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.City, GConst.InvalidId), status.Message);
        }
    }
}
