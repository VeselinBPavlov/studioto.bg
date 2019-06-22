namespace Studio.Application.Tests.Industries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Xunit;

    [CollectionDefinition("CommandCollection")]
    public class CreateIndustryCommandHandlerTests : BaseCommandTests
    {
        public CreateIndustryCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldCreateIndustry()
        {
            var sut = new CreateIndustryCommandHandler(Fixture.Context, Fixture.Mediator);

            var status = Record.ExceptionAsync(async () => await sut.Handle(new CreateIndustryCommand { Name = "Hairstyle" }, CancellationToken.None));

            Assert.Null(status.Exception);
            Assert.Equal("RanToCompletion", status.Status.ToString());
            Assert.Equal(1, this.Fixture.Context.Industries.Count());
        }
    }
}
