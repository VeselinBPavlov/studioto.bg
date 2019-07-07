namespace Studio.Application.Tests.Clients.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Clients.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    public class CreateClientCommandNotificationTests : CommandTestBase
    {
        private Mock<IMediator> mediator;
        private CreateClientCommandHandler sut;

        public CreateClientCommandNotificationTests()
        {
            mediator = new Mock<IMediator>();
            sut = new CreateClientCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseClientCreatedNotification()
        {
            var result = sut.Handle(new CreateClientCommand { CompanyName = GConst.ValidName}, CancellationToken.None);
            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateClientCommandNotification>(c => c.ClientId == clientId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}