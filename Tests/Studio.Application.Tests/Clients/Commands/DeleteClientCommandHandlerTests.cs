namespace Studio.Application.Tests.Clients.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Clients.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteClientCommandHandlerTests : CommandTestBase
    {
        private int clientId;
        private DeleteClientCommandHandler sut;

        public DeleteClientCommandHandlerTests()
        {
            clientId = CommandArrangeHelper.GetClientId(context);
            sut = new DeleteClientCommandHandler(context);
        }

        [Fact]
        public async Task ShouldDeleteClient()
        {            
            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));            

            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            //Assert.Equal(GConst.ValidCount, context.Clients.Count());
        }

        [Fact]
        public async Task ShouldТhrowDeleteFailureException()
        {
            CommandArrangeHelper.GetLocationId(context, clientId, null);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
            var message = status.Message;

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Client, clientId, GConst.Locations, GConst.ClientLower), message);
        }

        [Fact]
        public async Task ShouldThrowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Client, GConst.InvalidId), status.Message);
        }
    }
}
