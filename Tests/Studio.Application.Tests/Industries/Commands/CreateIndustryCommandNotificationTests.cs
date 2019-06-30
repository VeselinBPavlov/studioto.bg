namespace Studio.Application.Tests.Industries.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    public class CreateIndustryCommandNotificationTests : CommandTestBase
    {
        [Fact]
        public void ShouldRaiseIndustryCreatedNotification()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateIndustryCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateIndustryCommand { Name = GConst.IndustryValidName}, CancellationToken.None);
            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.IndustryValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateIndustryCommandNotification>(c => c.IndustryId == industryId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}