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
            var industry = new Industry { Name = GConst.ValidName };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            var sut = new DeleteIndustryCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailureExceptionForConnectedService()
        {
            var industryId = GetIndustryId();

            GetServiceId(industryId);

            var sut = new DeleteIndustryCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Industry, industryId, GConst.Services, GConst.IndustryLower), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailueExceptionForConnectedLocation()
        {
            var industryId = GetIndustryId();

            var locationId = GetLocationId(null, null);

            AddLocationIndustry(industryId, locationId);

            var sut = new DeleteIndustryCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Industry, industryId, GConst.Locations, GConst.IndustryLower), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowNotFoundException()
        {
            var sut = new DeleteIndustryCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Industry, GConst.InvalidId), status.Message);
        }
    }
}
