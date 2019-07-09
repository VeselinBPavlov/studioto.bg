namespace Studio.Application.Tests.Services.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Services.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateServiceCommandNotificationTests : CommandTestBase
    {
        private int industryId;
        private Mock<IMediator> mediator;
        private CreateServiceCommandHandler sut;

        public CreateServiceCommandNotificationTests()
        {
            industryId = CommandArrangeHelper.GetIndustryId(context);
            mediator = new Mock<IMediator>();
            sut = new CreateServiceCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseServiceCreatedNotification()
        {
            var result = sut.Handle(new CreateServiceCommand { Name = GConst.ValidName, IndustryId = industryId }, CancellationToken.None);
            var ServiceId = context.Services.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateServiceCommandNotification>(c => c.ServiceId == ServiceId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}