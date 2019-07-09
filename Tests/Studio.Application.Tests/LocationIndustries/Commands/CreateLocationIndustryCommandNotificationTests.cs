namespace Studio.Application.Tests.LocationIndustries.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.LocationIndustries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateLocationIndustryCommandNotificationTests : CommandTestBase
    {
        private int locationId;
        private int industryId;
        private Mock<IMediator> mediator;
        private CreateLocationIndustryCommandHandler sut;

        public CreateLocationIndustryCommandNotificationTests()
        {
            locationId = CommandArrangeHelper.GetLocationId(context, null, null);
            industryId = CommandArrangeHelper.GetIndustryId(context);
            mediator = new Mock<IMediator>();
            sut = new CreateLocationIndustryCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseLocationIndustryCreatedNotification()
        {
            var result = sut.Handle(new CreateLocationIndustryCommand { Description = GConst.ValidName, LocationId = locationId, IndustryId = industryId }, CancellationToken.None);

            locationId = context.LocationIndustries.SingleOrDefault(x => x.Description == GConst.ValidName).LocationId;
            industryId = context.LocationIndustries.SingleOrDefault(x => x.Description == GConst.ValidName).IndustryId;

            mediator.Verify(m => m.Publish(It.Is<CreateLocationIndustryCommandNotification>(c => c.LocationId == locationId && c.IndustryId == industryId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}