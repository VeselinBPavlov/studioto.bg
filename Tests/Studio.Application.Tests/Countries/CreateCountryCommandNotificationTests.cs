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
        private Mock<IMediator> mediator;
        private CreateCountryCommandHandler sut;

        public CreateCountryCommandNotificationTests()
        {
            mediator = new Mock<IMediator>();
            sut = new CreateCountryCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseCountryCreatedNotification()
        {
            var result = sut.Handle(new CreateCountryCommand { Name = GConst.ValidName}, CancellationToken.None);
            var countryId = context.Countries.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateCountryCommandNotification>(c => c.CountryId == countryId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}