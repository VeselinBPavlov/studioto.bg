namespace Studio.Application.Tests.Industries.Commands
{
    using Studio.Application.Industries.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using Xunit;

    public class UpdateIndustryCommandHandlerTests : BaseCommandTests
    {
        private string NotFoundExceptionMessage = "Entity \"Industry\" ({0}) was not found.";
        
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

            var industryId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == "Hairstyle").Id;

            var sut = new UpdateIndustryCommandHandler(this.Fixture.Context);
            var updatedIndustry = new UpdateIndustryCommand { Id = industryId, Name = "Fitness" };

            var status = Record.ExceptionAsync(async () => await sut.Handle(updatedIndustry, CancellationToken.None));

            var resultId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == "Fitness").Id;

            Assert.Equal(industryId, resultId);
            Assert.Equal(1, this.Fixture.Context.Industries.Count());
        }

        [Fact]
        public async void ShouldThrowNotFoundException()
        {
            var sut = new UpdateIndustryCommandHandler(this.Fixture.Context);
            var updatedIndustry = new UpdateIndustryCommand { Id = 100, Name = "Fitness" };

            var status = Record.ExceptionAsync(async () => await sut.Handle(updatedIndustry, CancellationToken.None));
                        
            var message = status.Result.Message;

            Assert.NotNull(status);
            Assert.Equal(string.Format(NotFoundExceptionMessage, 100), message);
            
        }
    }
}
