namespace Studio.Application.Tests.Addresses.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Addresses.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Domain.ValueObjects;
    using Xunit;

    public class DeleteAddressCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteAddress()
        {
            var inputAddressData = new InputAddressData
            {
                Street = "Iskar",
                Number = "13"
            };

            var address = new Address { AddressFormat = AddressFormat.For(inputAddressData) };

            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var addressId = context.Addresses.SingleOrDefault(x => x.AddressFormat.Street == "Iskar").Id;

            var sut = new DeleteAddressCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteAddressCommand { Id = addressId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task AddressShouldТhrowDeleteFailureException()
        {
            var inputAddressData = new InputAddressData
            {
                Street = "Iskar",
                Number = "13"
            };

            var address = new Address { AddressFormat = AddressFormat.For(inputAddressData) };

            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var addressId = context.Addresses.SingleOrDefault(x => x.AddressFormat.Street == "Iskar").Id;

            var location = new Location { Name = "H2B", AddressId = addressId };
            context.Locations.Add(location);
            await context.SaveChangesAsync();

            var sut = new DeleteAddressCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAddressCommand { Id = addressId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.DeleteFailureExceptionMessage, nameof(Address), addressId, "location", "address"), status.Message);
        }

        [Fact]
        public async Task AddressShouldТhrowNotFoundException()
        {
            var sut = new DeleteAddressCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAddressCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.NotFoundExceptionMessage, nameof(Address), GlobalConstants.InvalidId), status.Message);
        }
    }
}
