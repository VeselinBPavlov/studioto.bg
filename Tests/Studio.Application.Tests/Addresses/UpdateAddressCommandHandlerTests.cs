namespace Studio.Application.Tests.Addresses.Commands
{
    using MediatR;
    using Studio.Application.Addresses.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateAddressCommandHandlerTests : CommandTestBase
    {
        private int cityId;
        private int addressId;
        private UpdateAddressCommandHandler sut;

        public UpdateAddressCommandHandlerTests()
        {
            cityId = ArrangeHelper.GetCityId(context, null);
            addressId = ArrangeHelper.GetAddressId(context, cityId);
            sut = new UpdateAddressCommandHandler(context);
        }

        [Fact]
        public async Task ShouldUpdateAddress()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new UpdateAddressCommand { Id = addressId, Street = GConst.ValidName, Number = GConst.ValidAddressNumber, CityId = cityId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Addresses.Count());
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionError()
        {           
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Address, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowUpdateFailureException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = addressId, Street = GConst.ValidName, Number = GConst.ValidAddressNumber, CityId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Address, GConst.ValidName, GConst.CityLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowAddressFormatError()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = addressId, CityId = cityId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ValueObjectExceptionMessage, GConst.Address), status.Message);
        }
    }
}
