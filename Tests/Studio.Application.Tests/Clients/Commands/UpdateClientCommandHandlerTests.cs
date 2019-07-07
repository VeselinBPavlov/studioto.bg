namespace Studio.Application.Tests.Clients.Commands
{
    using MediatR;
    using Studio.Application.Clients.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
  
    public class UpdateClientCommandHandlerTests : CommandTestBase
    {
        private int clientId;
        private UpdateClientCommandHandler sut;

        public UpdateClientCommandHandlerTests()
        {
            clientId = GetClientId();
            sut = new UpdateClientCommandHandler(context);
        }

        [Fact]
        public async void ShouldUpdateCorrect()
        {           
            var updatedClient = new UpdateClientCommand { Id = clientId, CompanyName = GConst.ValidName };

            var status = Task.FromResult(await sut.Handle(updatedClient, CancellationToken.None));

            var resultId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ValidName).Id;

            Assert.Equal(clientId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Clients.Count());
        }

        [Fact]
        public async void ShouldThrowNotFoundException()
        {           
            var updatedClient = new UpdateClientCommand { Id = GConst.InvalidId, CompanyName = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedClient, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Client, GConst.InvalidId), status.Message);
        }
    }
}
