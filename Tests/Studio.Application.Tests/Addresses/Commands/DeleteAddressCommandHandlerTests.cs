namespace Studio.Application.Tests.Addresses.Commands
{
    using MediatR;
    using Studio.Application.Addresses.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class DeleteAddressCommandHandlerTests : CommandTestBase
    {
        private int addressId;
        private DeleteAddressCommandHandler sut;

        public DeleteAddressCommandHandlerTests()
        {
            addressId = CommandArrangeHelper.GetAddressId(context, null);
            sut = new DeleteAddressCommandHandler(context);            
        }

        [Fact]
        public async Task ShouldDeleteAddress()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteAddressCommand { Id = addressId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task AddressShouldТhrowDeleteFailureException()
        {
            CommandArrangeHelper.GetLocationId(context, null, addressId);
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAddressCommand { Id = addressId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Address, addressId, GConst.LocationLower, GConst.AddressLower), status.Message);
        }

        [Fact]
        public async Task AddressShouldТhrowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAddressCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Address, GConst.InvalidId), status.Message);
        }
    }
}
