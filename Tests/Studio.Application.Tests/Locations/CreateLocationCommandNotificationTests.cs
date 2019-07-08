namespace Studio.Application.Tests.Locations.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Locations.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateLocationCommandNotificationTests : CommandTestBase
    {
        private int clientId;
        private int addressId;
        private Mock<IMediator> mediator;
        private CreateLocationCommandHandler sut;

        public CreateLocationCommandNotificationTests()
        {
            clientId = GetClientId();
            addressId = GetAddressId(null);
            mediator = new Mock<IMediator>();
            sut = new CreateLocationCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseLocationCreatedNotification()
        {
            var result = sut.Handle(new CreateLocationCommand 
            { 
                Name = GConst.ValidName,       
                StartDay = GConst.ValidStartDay,
                EndDay = GConst.ValidEndDay,
                ClientId = clientId, 
                AddressId = addressId 
            }, CancellationToken.None);

            var LocationId = context.Locations.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateLocationCommandNotification>(c => c.LocationId == LocationId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}