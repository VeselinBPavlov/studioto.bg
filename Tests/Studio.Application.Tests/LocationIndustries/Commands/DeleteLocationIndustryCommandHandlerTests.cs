namespace Studio.Application.Tests.LocationIndustries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.LocationIndustries.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteLocationIndustryCommandHandlerTests : CommandTestBase
    {
        private int locationId;
        private int industryId;
        private DeleteLocationIndustryCommandHandler sut;

        public DeleteLocationIndustryCommandHandlerTests()
        {
            locationId = CommandArrangeHelper.GetLocationId(context, null, null);
            industryId = CommandArrangeHelper.GetIndustryId(context);
            sut = new DeleteLocationIndustryCommandHandler(context);
        }

        [Fact]
        public async Task ShouldDeleteLocationIndustry()
        {
            CommandArrangeHelper.AddLocationIndustry(context, industryId, locationId);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteLocationIndustryCommand { LocationId = locationId, IndustryId = industryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task LocationIndustrieshouldТhrowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteLocationIndustryCommand { LocationId = GConst.InvalidId, IndustryId = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.LocationIndustry, $"{GConst.InvalidId} - {GConst.InvalidId}"), status.Message);
        }
    }
}
