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
        private int countryId;
        private Mock<IMediator> mediator;
        private CreateCityCommandHandler sut;

        public CreateCityCommandNotificationTests()
        {
            countryId = CommandArrangeHelper.GetCountryId(context);
            mediator = new Mock<IMediator>();
            sut = new CreateCityCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseCityCreatedNotification()
        {
            var result = sut.Handle(new CreateCityCommand { Name = GConst.ValidName, CountryId = countryId }, CancellationToken.None);

            var cityId = context.Cities.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateCityCommandNotification>(c => c.CityId == cityId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}