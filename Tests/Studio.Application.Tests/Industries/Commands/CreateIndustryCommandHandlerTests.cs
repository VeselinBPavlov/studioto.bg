namespace Studio.Application.Tests.Industries.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Shouldly;
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

            var result = await sut.Handle(new CreateIndustryCommand { Name = "Hairstyle" }, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
            result.ShouldNotBeNull();
        }
    }
}
