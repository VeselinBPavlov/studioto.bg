namespace Studio.Application.Tests.Industries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Cities.Commands.Delete;
    using Studio.Application.Industries.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteIndustryCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteIndustry()
        {
            var industry = new Industry { Name = GConst.IndustryValidName };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.IndustryValidName).Id;

            var sut = new DeleteIndustryCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailureExceptionForConnectedService()
        {
            var industry = new Industry { Name = "Beauty" };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == "Beauty").Id;

            var service = new Service { Name = GConst.ServiceValidName, IndustryId = industryId };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var sut = new DeleteIndustryCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, nameof(Industry), industryId, "services", "industry"), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailueExceptionForConnectedLocation()
        {
            var industry = new Industry { Name = GConst.IndustrySecondValidName };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.IndustrySecondValidName).Id;

            var location = new Location { Name = "Location" };
            context.Locations.Add(location);
            await context.SaveChangesAsync();

            var locationId = context.Locations.SingleOrDefault(x => x.Name == "Location").Id;

            var locationIndustry = new LocationIndustry { IndustryId = industryId, LocationId = locationId };
            context.LocationIndustries.Add(locationIndustry);
            await context.SaveChangesAsync();

            var sut = new DeleteIndustryCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, nameof(Industry), industryId, "locations", "industry"), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowNotFoundException()
        {
            var sut = new DeleteIndustryCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, nameof(Industry), GConst.InvalidId), status.Message);
        }
    }
}
