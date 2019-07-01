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
        [Fact]
        public void ShouldRaiseAddressCreatedNotification()
        {
            int cityId = GetCityId(null);

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateAddressCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateAddressCommand { Street = GConst.ValidName, Number = GConst.ValidAddressNumber, Latitude = 40.00M, CityId = cityId }, CancellationToken.None);
            var addressId = context.Addresses.SingleOrDefault(x => x.Latitude == 40.00M).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateAddressCommandNotification>(c => c.AddressId == addressId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}