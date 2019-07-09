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
        private int cityId;
        private DeleteCityCommandHandler sut;

        public DeleteCityCommandHandlerTests()
        {
            cityId = CommandArrangeHelper.GetCityId(context, null);
            sut = new DeleteCityCommandHandler(context);
        }

        [Fact]
        public async Task ShouldDeleteCity()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteCityCommand { Id = cityId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task CityShouldТhrowDeleteFailureException()
        {
            CommandArrangeHelper.GetAddressId(context, cityId);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCityCommand { Id = cityId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.City, cityId, GConst.Addresses, GConst.CityLower), status.Message);
        }

        [Fact]
        public async Task CityShouldТhrowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCityCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.City, GConst.InvalidId), status.Message);
        }
    }
}
