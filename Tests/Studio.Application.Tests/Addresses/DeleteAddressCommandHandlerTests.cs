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
        [Fact]
        public async Task ShouldDeleteAddress()
        {
            var addressId = GetAddressId(null);

            var sut = new DeleteAddressCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteAddressCommand { Id = addressId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task AddressShouldТhrowDeleteFailureException()
        {
            var addressId = GetAddressId(null);

            GetLocationId(null, addressId);

            var sut = new DeleteAddressCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAddressCommand { Id = addressId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Address, addressId, GConst.LocationLower, GConst.AddressLower), status.Message);
        }

        [Fact]
        public async Task AddressShouldТhrowNotFoundException()
        {
            var sut = new DeleteAddressCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAddressCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Address, GConst.InvalidId), status.Message);
        }
    }
}
