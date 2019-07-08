namespace Studio.Application.Tests.LocationIndustries.Commands
{
    using MediatR;
    using Studio.Application.LocationIndustries.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateLocationIndustrieCommandHandlerTests : CommandTestBase
    {
        private int locationId;
        private int industryId;
        private UpdateLocationIndustryCommandHandler sut;

        public UpdateLocationIndustrieCommandHandlerTests()
        {
            locationId = ArrangeHelper.GetLocationId(context, null, null);
            industryId = ArrangeHelper.GetIndustryId(context);
            sut = new UpdateLocationIndustryCommandHandler(context);
        }

        [Fact]
        public async void LocationIndustryShouldUpdateCorrect()
        {
            ArrangeHelper.AddLocationIndustry(context, industryId, locationId);

            var updatedLocationIndustrie = new UpdateLocationIndustryCommand { LocationId = locationId, IndustryId = industryId, Description = GConst.ValidName };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedLocationIndustrie, CancellationToken.None));

            var locationIdResult = context.LocationIndustries.SingleOrDefault(x => x.Description == GConst.ValidName).LocationId;
            var industryIdResult = context.LocationIndustries.SingleOrDefault(x => x.Description == GConst.ValidName).IndustryId;

            Assert.Equal(locationId, locationIdResult);
            Assert.Equal(industryId, industryIdResult);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.LocationIndustries.Count());
        }

        [Fact]
        public async void LocationIndustryShouldThrowNotFoundException()
        {
            var updatedLocationIndustry = new UpdateLocationIndustryCommand { LocationId = GConst.InvalidId, IndustryId = GConst.InvalidId, Description = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedLocationIndustry, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.LocationIndustry, $"{GConst.InvalidId} - {GConst.InvalidId}"), status.Message);            
        }
    }
}
