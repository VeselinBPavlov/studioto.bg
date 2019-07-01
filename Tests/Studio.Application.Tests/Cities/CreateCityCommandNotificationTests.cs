namespace Studio.Application.Tests.Cities.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Cities.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateCityCommandNotificationTests : CommandTestBase
    {
        [Fact]
        public void ShouldRaiseCityCreatedNotification()
        {
            var countryId = GetCountryId();

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateCityCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateCityCommand { Name = GConst.ValidName, CountryId = countryId }, CancellationToken.None);

            var cityId = context.Cities.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateCityCommandNotification>(c => c.CityId == cityId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}