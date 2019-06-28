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

    public class CreateClientCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateClient()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateClientCommandHandler(context, mediator.Object);

            var status = Task.FromResult(await sut.Handle(new CreateClientCommand { CompanyName = GlobalConstants.ClientValidName }, CancellationToken.None));

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientValidName).Id;
            
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Clients.Count());
        }
    }
}
