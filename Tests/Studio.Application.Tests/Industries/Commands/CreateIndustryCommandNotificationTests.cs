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
        private Mock<IMediator> mediator;
        private CreateIndustryCommandHandler sut;

        public CreateIndustryCommandNotificationTests()
        {
            mediator = new Mock<IMediator>();
            sut = new CreateIndustryCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseIndustryCreatedNotification()
        {
            var result = sut.Handle(new CreateIndustryCommand { Name = GConst.ValidName}, CancellationToken.None);
            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateIndustryCommandNotification>(c => c.IndustryId == industryId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}