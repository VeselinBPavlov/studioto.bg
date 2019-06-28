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

    [CollectionDefinition("ClientCollection")]
    public class DeleteClientCommandHandlerTests : BaseCommandTests
    {
        public DeleteClientCommandHandlerTests(CommandTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldDeleteClient()
        {
            var client = new Client { CompanyName = GlobalConstants.ClientValidName };

            Fixture.Context.Clients.Add(client);
            await Fixture.Context.SaveChangesAsync();
            var count = Fixture.Context.Clients.Count();

            var clientId = Fixture.Context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientValidName).Id;

            var sut = new DeleteClientCommandHandler(Fixture.Context);

            var status = Task.FromResult(await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));

            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task ShouldТhrowDeleteFailureException()
        {
            var client = new Client { CompanyName = GlobalConstants.ClientSecondValidName };

            Fixture.Context.Clients.Add(client);
            await Fixture.Context.SaveChangesAsync();
            var count = Fixture.Context.Clients.Count();

            var clientId = Fixture.Context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientSecondValidName).Id;

            var location = new Location
            {
                Name = GlobalConstants.LocationValidName,
                ClientId = clientId
            };

            Fixture.Context.Locations.Add(location);
            await Fixture.Context.SaveChangesAsync();

            var sut = new DeleteClientCommandHandler(Fixture.Context);

            var status = Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
            var message = status.Result.Message;

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientDeleteFalueExceptionMessage, clientId), message);
        }

        [Fact]
        public async Task ShouldThrowNotFoundException()
        {
            var sut = new DeleteClientCommandHandler(Fixture.Context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldТhrowExceptionOnDeleteAlreadyDeletedObject()
        {
            var client = new Client { CompanyName = GlobalConstants.ClientThirdValidName };

            Fixture.Context.Clients.Add(client);
            await Fixture.Context.SaveChangesAsync();

            var clientFromDb = Fixture.Context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientThirdValidName);
            var clientId = clientFromDb.Id;
            clientFromDb.IsDeleted = true;
            await Fixture.Context.SaveChangesAsync();

            var sut = new DeleteClientCommandHandler(Fixture.Context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientDeleteFalueIsDeletedTrue, clientId), status.Message);
        }
    }
}
