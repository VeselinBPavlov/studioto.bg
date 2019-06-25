namespace Studio.Application.Tests.Industries.Commands
{
    using MediatR;
    using Studio.Application.Industries.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateClientCommandHandlerTests : BaseCommandTests
    {        
        public UpdateClientCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async void ShouldUpdateCorrect()
        {
            var client = new Client { CompanyName = GlobalConstants.ClientValidName };

            this.Fixture.Context.Clients.Add(client);
            await this.Fixture.Context.SaveChangesAsync();

            var clientId = this.Fixture.Context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientValidName).Id;

            var sut = new UpdateClientCommandHandler(this.Fixture.Context);
            var updatedClient = new UpdateClientCommand { Id = clientId, CompanyName = GlobalConstants.ClientSecondValidName };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedClient, CancellationToken.None));

            var resultId = this.Fixture.Context.Clients.SingleOrDefault(x => x.CompanyName == GlobalConstants.ClientSecondValidName).Id;

            Assert.Equal(clientId, resultId);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, this.Fixture.Context.Clients.Count());
        }

        [Fact]
        public async void ShouldThrowNotFoundException()
        {
            var sut = new UpdateClientCommandHandler(this.Fixture.Context);
            var updatedClient = new UpdateClientCommand { Id = GlobalConstants.InvalidId, CompanyName = GlobalConstants.ClientSecondValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedClient, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ClientNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);            
        }
    }
}
