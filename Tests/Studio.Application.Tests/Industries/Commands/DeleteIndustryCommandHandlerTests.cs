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
    public class DeleteIndustryCommandHandlerTests : BaseCommandTests
    {
        public DeleteIndustryCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldDeleteIndustry()
        {
            var industry = new Industry { Name = GlobalConstants.IndustryValidName };

            this.Fixture.Context.Industries.Add(industry);
            await this.Fixture.Context.SaveChangesAsync();

            var industryId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == GlobalConstants.IndustryValidName).Id;

            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailureExceptionForConnectedService()
        {
            var industry = new Industry { Name = "Beauty" };

            this.Fixture.Context.Industries.Add(industry);
            await this.Fixture.Context.SaveChangesAsync();

            var industryId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == "Beauty").Id;

            var service = new Service { Name = GlobalConstants.ServiceValidName, IndustryId = industryId };
            this.Fixture.Context.Services.Add(service);
            await this.Fixture.Context.SaveChangesAsync();

            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.IndustryDeleteFalueExceptionMessageService, industryId), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailueExceptionForConnectedLocation()
        {
            var industry = new Industry { Name = GlobalConstants.IndustrySecondValidName };

            this.Fixture.Context.Industries.Add(industry);
            await this.Fixture.Context.SaveChangesAsync();

            var industryId = this.Fixture.Context.Industries.SingleOrDefault(x => x.Name == GlobalConstants.IndustrySecondValidName).Id;

            var location = new Location { Name = "Location" };
            this.Fixture.Context.Locations.Add(location);
            await this.Fixture.Context.SaveChangesAsync();

            var locationId = this.Fixture.Context.Locations.SingleOrDefault(x => x.Name == "Location").Id;

            var locationIndustry = new LocationIndustry { IndustryId = industryId, LocationId = locationId };
            this.Fixture.Context.LocationIndustries.Add(locationIndustry);
            await this.Fixture.Context.SaveChangesAsync();

            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.IndustryDeleteFalueExceptionMessageLocation, industryId), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowNotFoundException()
        {
            var sut = new DeleteIndustryCommandHandler(this.Fixture.Context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.IndustryNotFoundExceptionMessage, GlobalConstants.InvalidId), status.Message);
        }
    }
}
