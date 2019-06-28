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
            var client = new Client { CompanyName = GlobalConstants.ClientValidName };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientValidName).Id;

            var sut = new UpdateClientCommandHandler(context);
            var updatedClient = new UpdateClientCommand { Id = clientId, CompanyName = GlobalConstants.ClientSecondValidName };

            var status = Task.FromResult(await sut.Handle(updatedClient, CancellationToken.None));

            var resultId = context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientSecondValidName).Id;

            Assert.Equal(clientId, resultId);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Clients.Count());
        }

        [Fact]
        public async void ShouldThrowNotFoundException()
        {
            var sut = new UpdateClientCommandHandler(context);
            var updatedClient = new UpdateClientCommand { Id = GlobalConstants.InvalidId, CompanyName = GlobalConstants.ClientSecondValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedClient, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);
        }
    }
}
