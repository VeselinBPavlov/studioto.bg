namespace Studio.Application.Tests.Industries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Industries.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    [CollectionDefinition("CommandCollection")]
    public class DeleteClientCommandHandlerTests : BaseCommandTests
    {
        public DeleteClientCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldDeleteClient()
        {
            var client = new Client { Name = GlobalConstants.ClientValidName };

            this.Fixture.Context.Clients.Add(client);
            await this.Fixture.Context.SaveChangesAsync();

            var clientId = this.Fixture.Context.Clients.SingleOrDefault(x => x.Name == GlobalConstants.ClientValidName).Id;

            var sut = new DeleteClientCommandHandler(this.Fixture.Context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, this.Fixture.Context.Clients.Count());
        }

        [Fact]
        public async Task ShouldТhrowDeleteFailureException()
        {
            var client = new Client { Name = GlobalConstants.ClientSecondValidName };

            this.Fixture.Context.Clients.Add(client);
            await this.Fixture.Context.SaveChangesAsync();

            var clientId = this.Fixture.Context.Clients.SingleOrDefault(x => x.Name == GlobalConstants.ClientSecondValidName).Id;


            var location = new Location
            {
                Name = GlobalConstants.LocationValidName,
                ClientId = clientId                
            };

            this.Fixture.Context.Locations.Add(location);
            await this.Fixture.Context.SaveChangesAsync();

            var sut = new DeleteClientCommandHandler(this.Fixture.Context);

            var status = Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = clientId }, CancellationToken.None));
            var message = status.Result.Message;

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientDeleteFalueExceptionMessage, clientId), message);
            Assert.Equal(1, this.Fixture.Context.Clients.Count());
        }

        [Fact]
        public async Task ShouldТhrowNotFoundException()
        {
            var sut = new DeleteClientCommandHandler(this.Fixture.Context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteClientCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);
            Assert.Equal(0, this.Fixture.Context.Industries.Count());
        }
    }
}
