namespace Studio.Application.Tests.Countries.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Countries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    public class CreateCountryCommandNotificationTests : CommandTestBase
    {
        [Fact]
        public void ShouldRaiseCountryCreatedNotification()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateCountryCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateCountryCommand { Name = GlobalConstants.CountryValidName}, CancellationToken.None);
            var countryId = context.Countries.SingleOrDefault(x => x.Name == GlobalConstants.CountryValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateCountryCommandNotification>(c => c.CountryId == countryId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}