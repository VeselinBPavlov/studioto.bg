namespace Studio.Application.Tests.Locations.Commands
{
    using MediatR;
    using Studio.Application.Locations.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateLocationCommandHandlerTests : CommandTestBase
    {
        private int clientId;
        private int addressId;
        private int locationId;
        private UpdateLocationCommandHandler sut;

        public UpdateLocationCommandHandlerTests()
        {
            clientId = GetClientId();
            addressId = GetAddressId(null);
            locationId = GetLocationId(clientId, addressId);
            sut = new UpdateLocationCommandHandler(context);
        }

        [Fact]
        public async void LocationShouldUpdateCorrect()
        {
            var updatedLocation = new UpdateLocationCommand 
            { 
                Id = locationId, 
                Name = GConst.ValidName, 
                StartDay = GConst.ValidStartDay,
                EndDay = GConst.ValidEndDay,
                ClientId = clientId, 
                AddressId = addressId 
            };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedLocation, CancellationToken.None));

            var resultId = context.Locations.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            Assert.Equal(locationId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Locations.Count());
        }

        [Fact]
        public async void LocationShouldThrowReferenceExceptionForInvalidClient()
        {
            var updatedLocation = new UpdateLocationCommand 
            { 
                Id = locationId, 
                Name = GConst.ValidName, 
                ClientId = GConst.InvalidId,
                AddressId = addressId
            };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedLocation, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Location, locationId, GConst.ClientLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void LocationShouldThrowReferenceExceptionForInvalidAddress()
        {
            var updatedLocation = new UpdateLocationCommand 
            { 
                Id = locationId, 
                Name = GConst.ValidName, 
                ClientId = clientId,
                AddressId = GConst.InvalidId 
            };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedLocation, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Location, locationId, GConst.AddressLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void LocationShouldThrowNotFoundException()
        {
            var updatedLocation = new UpdateLocationCommand { Id = GConst.InvalidId, Name = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedLocation, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Location, GConst.InvalidId), status.Message);            
        }
    }
}
