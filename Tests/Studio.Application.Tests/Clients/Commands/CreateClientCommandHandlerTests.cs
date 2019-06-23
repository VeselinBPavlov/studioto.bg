namespace Studio.Application.Tests.Industries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    [CollectionDefinition("CommandCollection")]
    public class CreateClientCommandHandlerTests : BaseCommandTests
    {
        public CreateClientCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldCreateClient()
        {
            var sut = new CreateClientCommandHandler(Fixture.Context, Fixture.Mediator);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateClientCommand { Name = GlobalConstants.ClientValidName }, CancellationToken.None));

            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, this.Fixture.Context.Clients.Count());
        }
    }
}
