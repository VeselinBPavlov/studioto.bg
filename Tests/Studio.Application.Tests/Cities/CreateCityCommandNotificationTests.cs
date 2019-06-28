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
            var country = new Country { Name = GlobalConstants.CountryValidName };
            context.Countries.Add(country);
            this.context.SaveChanges();
            var countryId = context.Countries.SingleOrDefault(x => x.Name == GlobalConstants.CountryValidName).Id;

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateCityCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateCityCommand { Name = GlobalConstants.CityValidName, CountryId = countryId }, CancellationToken.None);
            var cityId = context.Cities.SingleOrDefault(x => x.Name == GlobalConstants.CityValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateCityCommandNotification>(c => c.CityId == cityId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}