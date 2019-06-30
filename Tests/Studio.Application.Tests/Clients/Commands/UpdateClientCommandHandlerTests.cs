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
        [Fact]
        public async void ShouldUpdateCorrect()
        {
            var client = new Client { CompanyName = GConst.ClientValidName };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ClientValidName).Id;

            var sut = new UpdateClientCommandHandler(context);
            var updatedClient = new UpdateClientCommand { Id = clientId, CompanyName = GConst.ClientSecondValidName };

            var status = Task.FromResult(await sut.Handle(updatedClient, CancellationToken.None));

            var resultId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ClientSecondValidName).Id;

            Assert.Equal(clientId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Clients.Count());
        }

        [Fact]
        public async void ShouldThrowNotFoundException()
        {
            var sut = new UpdateClientCommandHandler(context);
            var updatedClient = new UpdateClientCommand { Id = GConst.InvalidId, CompanyName = GConst.ClientSecondValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedClient, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, nameof(Client), GConst.InvalidId), status.Message);
        }
    }
}
