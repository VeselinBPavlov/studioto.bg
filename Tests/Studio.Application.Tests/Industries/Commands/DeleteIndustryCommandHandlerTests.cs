namespace Studio.Application.Tests.Industries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Application.Industries.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Domain.Entities;
    using Xunit;

    [CollectionDefinition("CommandCollection")]
    public class DeleteIndustryCommandHandlerTests : BaseCommandTests
    {
        private const int InvalidId = 100;
        private const int ValidId = 1;
        private const string ServiceName = "Haircut";
        private string NotFoundExceptionMessage = "Entity \"Industry\" ({0}) was not found.";
        private string DeleteFalueExceptionMessage = "Deletion of entity \"Service\" ({0}) failed. There are existing services associated with this industry.";

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

            var industryId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == "Hairstyle").Id;

            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);

            var status = Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal("RanToCompletion", status.Status.ToString());
            Assert.Equal(0, this.Fixture.Context.Industries.Count());
        }

        [Fact]
        public async Task ShouldТhrowDeleteFailureException()
        {
            var industry = new Industry { Name = "Fitness" };

            this.Fixture.Context.Industries.Add(industry);
            await this.Fixture.Context.SaveChangesAsync();

            var industryId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == "Fitness").Id;

            var service = new Service { Name = ServiceName, IndustryId = industryId };
            this.Fixture.Context.Services.Add(service);
            await this.Fixture.Context.SaveChangesAsync();

            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);
            
            var status = Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            var message = status.Result.Message;

            Assert.NotNull(status);
            Assert.Equal(string.Format(DeleteFalueExceptionMessage, industryId), message);
            Assert.Equal(1, this.Fixture.Context.Industries.Count());
        }

        [Fact]
        public async Task ShouldТhrowNotFoundException()
        {
            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);           

            var status = Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = InvalidId }, CancellationToken.None));
            var message = status.Result.Message;
                      
            Assert.NotNull(status);
            Assert.Equal(string.Format(NotFoundExceptionMessage, InvalidId), message);
            Assert.Equal(0, this.Fixture.Context.Industries.Count());
        }
    }
}
