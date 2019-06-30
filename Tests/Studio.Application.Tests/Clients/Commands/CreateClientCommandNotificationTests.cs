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
        [Fact]
        public void ShouldRaiseClientCreatedNotification()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateClientCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateClientCommand { CompanyName = GConst.ClientValidName}, CancellationToken.None);
            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ClientValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateClientCommandNotification>(c => c.ClientId == clientId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}