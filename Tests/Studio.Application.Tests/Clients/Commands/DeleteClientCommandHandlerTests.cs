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
        [Fact]
        public async Task ShouldDeleteClient()
        {
            var client = new Client { CompanyName = GConst.ClientValidName };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ClientValidName).Id;

            var sut = new DeleteClientCommandHandler(context);

            var status = Task.FromResult(await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
            var resultCount = context.Clients.Count();

            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task ShouldТhrowDeleteFailureException()
        {
            var client = new Client { CompanyName = GConst.ClientSecondValidName };

            context.Clients.Add(client);
            await context.SaveChangesAsync();
            var count = context.Clients.Count();

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ClientSecondValidName).Id;

            var location = new Location
            {
                Name = GConst.LocationValidName,
                ClientId = clientId
            };

            context.Locations.Add(location);
            await context.SaveChangesAsync();

            var sut = new DeleteClientCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
            var message = status.Message;

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, nameof(Client), clientId, "locations", "client"), message);
        }

        [Fact]
        public async Task ShouldThrowNotFoundException()
        {
            var sut = new DeleteClientCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, nameof(Client), GConst.InvalidId), status.Message);
        }
    }
}
