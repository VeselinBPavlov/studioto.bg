namespace Studio.Application.Tests.Industries.Commands
{
    using MediatR;
    using Shouldly;
    using Studio.Application.Industries.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using Xunit;

    public class UpdateIndustryCommandHandlerTests : BaseCommandTests
    {
        public UpdateIndustryCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async void ShouldUpdateCorrect()
        {
            var industry = new Industry { Name = "Hairstyle" };

            this.Fixture.Context.Industries.Add(industry);
            await this.Fixture.Context.SaveChangesAsync();            

            var sut = new UpdateIndustryCommandHandler(this.Fixture.Context);
            var updatedIndustry = new UpdateIndustryCommand { Id = 1, Name = "Fitness" };

            var result = await sut.Handle(updatedIndustry, CancellationToken.None);

            Assert.Equal(1, this.Fixture.Context.Industries.Count());
            result.ShouldBeOfType<Unit>();
            result.ShouldNotBeNull();
        }
    }
}
