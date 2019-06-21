namespace Studio.Application.Tests.Industries.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Shouldly;
    using Studio.Application.Industries.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Domain.Entities;
    using Xunit;

    [CollectionDefinition("CommandCollection")]
    public class DeleteIndustryCommandHandlerTests : BaseCommandTests
    {
        public DeleteIndustryCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldDeleteIndustry()
        {
            var industry = new Industry { Name = "Hairstyle" };

            this.Fixture.Context.Industries.Add(industry);
            await this.Fixture.Context.SaveChangesAsync();

            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);

            var result = await sut.Handle(new DeleteIndustryCommand { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
            result.ShouldNotBeNull();
        }
    }
}
