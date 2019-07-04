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
        [Fact]
        public void ShouldRaiseLocationIndustryCreatedNotification()
        {
            var locationId = GetLocationId(null, null);
            var industryId = GetIndustryId();

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateLocationIndustryCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateLocationIndustryCommand { Description = GConst.ValidName, LocationId = locationId, IndustryId = industryId }, CancellationToken.None);

            locationId = context.LocationIndustries.SingleOrDefault(x => x.Description == GConst.ValidName).LocationId;
            industryId = context.LocationIndustries.SingleOrDefault(x => x.Description == GConst.ValidName).IndustryId;


            mediatorMock.Verify(m => m.Publish(It.Is<CreateLocationIndustryCommandNotification>(c => c.LocationId == locationId && c.IndustryId == industryId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}