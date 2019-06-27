namespace Studio.Application.Tests.Clients.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Moq;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using System.Data;
    using System;
    using Studio.Application.Clients.Commands.Create;

    [CollectionDefinition("CommandCollection")]
    public class CreateClientCommandHandlerTests : BaseCommandTests
    {
        public CreateClientCommandHandlerTests(CommandTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldCreateClient()
        {
            var sut = new CreateClientCommandHandler(Fixture.Context, Fixture.Mediator);

            var status = Task.FromResult(await sut.Handle(new CreateClientCommand { CompanyName = GlobalConstants.ClientValidName }, CancellationToken.None));

            var clientId = Fixture.Context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientValidName).Id;

            //this.Fixture.Mock.Verify(m => m.Publish(It.Is<CreateClientCommandNotification>(c => c.ClientId == clientId), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, Fixture.Context.Clients.Count());
        }
    }
}
