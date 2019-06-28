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
            var client = new Client { CompanyName = GlobalConstants.ClientValidName };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientValidName).Id;

            var sut = new DeleteClientCommandHandler(context);

            var status = Task.FromResult(await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
            var resultCount = context.Clients.Count();

            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task ShouldТhrowDeleteFailureException()
        {
            var client = new Client { CompanyName = GlobalConstants.ClientSecondValidName };

            context.Clients.Add(client);
            await context.SaveChangesAsync();
            var count = context.Clients.Count();

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientSecondValidName).Id;

            var location = new Location
            {
                Name = GlobalConstants.LocationValidName,
                ClientId = clientId
            };

            context.Locations.Add(location);
            await context.SaveChangesAsync();

            var sut = new DeleteClientCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
            var message = status.Message;

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientDeleteFalueExceptionMessage, clientId), message);
        }

        [Fact]
        public async Task ShouldThrowNotFoundException()
        {
            var sut = new DeleteClientCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldТhrowExceptionOnDeleteAlreadyDeletedObject()
        {
            var client = new Client { CompanyName = GlobalConstants.ClientThirdValidName };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var clientFromDb = context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientThirdValidName);
            var clientId = clientFromDb.Id;
            clientFromDb.IsDeleted = true;
            await context.SaveChangesAsync();

            var sut = new DeleteClientCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientDeleteFalueIsDeletedTrue, clientId), status.Message);
        }
    }
}
