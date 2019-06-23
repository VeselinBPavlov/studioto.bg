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

    public class UpdateIndustryCommandHandlerTests : BaseCommandTests
    {        
        public UpdateIndustryCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async void ShouldUpdateCorrect()
        {
            var industry = new Industry { Name = GlobalConstants.IndustryValidName };

            this.Fixture.Context.Industries.Add(industry);
            await this.Fixture.Context.SaveChangesAsync();

            var industryId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == GlobalConstants.IndustryValidName).Id;

            var sut = new UpdateIndustryCommandHandler(this.Fixture.Context);
            var updatedIndustry = new UpdateIndustryCommand { Id = industryId, Name = GlobalConstants.IndustrySecondValidName };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedIndustry, CancellationToken.None));

            var resultId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == GlobalConstants.IndustrySecondValidName).Id;

            Assert.Equal(industryId, resultId);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, this.Fixture.Context.Industries.Count());
        }

        [Fact]
        public async void ShouldThrowNotFoundException()
        {
            var sut = new UpdateIndustryCommandHandler(this.Fixture.Context);
            var updatedIndustry = new UpdateIndustryCommand { Id = 100, Name = GlobalConstants.IndustrySecondValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedIndustry, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.IndustryNotFoundExceptionMessage, 100), status.Message);
            
        }
    }
}
