namespace Studio.Application.Tests.Locations.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Locations.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteLocationCommandHandlerTests : CommandTestBase
    {
        private int locationId;
        private int addressId;
        private DeleteLocationCommandHandler sut;

        public DeleteLocationCommandHandlerTests()
        {
            addressId = CommandArrangeHelper.GetAddressId(context, null);
            locationId = CommandArrangeHelper.GetLocationId(context, null, addressId);
            sut = new DeleteLocationCommandHandler(context);
        }

        [Fact]
        public async Task ShouldDeleteLocation()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteLocationCommand { Id = locationId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task LocationShouldТhrowDeleteFailureExceptionForInvalidClient()
        {
            CommandArrangeHelper.GetEmployeeId(context, locationId);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteLocationCommand { Id = locationId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Location, locationId, GConst.Employees, GConst.LocationLower), status.Message);
        }

        [Fact]
        public async Task LocationShouldТhrowDeleteFailureExceptionForInvalidAddress()
        {
            var industryId = CommandArrangeHelper.GetIndustryId(context);
            CommandArrangeHelper.AddLocationIndustry(context, industryId, locationId);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteLocationCommand { Id = locationId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Location, locationId, GConst.Industries, GConst.LocationLower), status.Message);
        }

        [Fact]
        public async Task LocationShouldТhrowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteLocationCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Location, GConst.InvalidId), status.Message);
        }
    }
}
