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

            var status = Task.FromResult(await sut.Handle(new CreateClientCommand { CompanyName = GConst.ValidName }, CancellationToken.None));

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ValidName).Id;
            
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Clients.Count());
        }
    }
}
