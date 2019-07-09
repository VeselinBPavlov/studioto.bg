namespace Studio.Application.Tests.Addresses.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Addresses.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateAddressCommandNotificationTests : CommandTestBase
    {
        private int cityId;
        private Mock<IMediator> mediator;
        private CreateAddressCommandHandler sut;

        public CreateAddressCommandNotificationTests()
        {
            cityId = CommandArrangeHelper.GetCityId(context, null);
            this.mediator = new Mock<IMediator>();
            this.sut = new CreateAddressCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseAddressCreatedNotification()
        {
            var result = sut.Handle(new CreateAddressCommand { Street = GConst.ValidName, Number = GConst.ValidAddressNumber, Latitude = 40.00M, CityId = cityId }, CancellationToken.None);
            var addressId = context.Addresses.SingleOrDefault(x => x.Latitude == 40.00M).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateAddressCommandNotification>(c => c.AddressId == addressId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}