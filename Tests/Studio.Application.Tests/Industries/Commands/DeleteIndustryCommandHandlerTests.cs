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
        private int industryId;
        private DeleteIndustryCommandHandler sut;

        public DeleteIndustryCommandHandlerTests()
        {
            industryId = ArrangeHelper.GetIndustryId(context);
            sut = new DeleteIndustryCommandHandler(context);
        }

        [Fact]
        public async Task ShouldDeleteIndustry()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailureExceptionForConnectedService()
        {
            ArrangeHelper.GetServiceId(context, industryId);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Industry, industryId, GConst.Services, GConst.IndustryLower), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowDeleteFailueExceptionForConnectedLocation()
        {
            var locationId = ArrangeHelper.GetLocationId(context, null, null);

            ArrangeHelper.AddLocationIndustry(context, industryId, locationId);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = industryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Industry, industryId, GConst.Locations, GConst.IndustryLower), status.Message);
        }

        [Fact]
        public async Task IndustryShouldТhrowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteIndustryCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Industry, GConst.InvalidId), status.Message);
        }
    }
}
