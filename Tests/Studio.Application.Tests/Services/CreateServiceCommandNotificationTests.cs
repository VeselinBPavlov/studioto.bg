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
        [Fact]
        public void ShouldRaiseServiceCreatedNotification()
        {
            var industryId = GetIndustryId();

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateServiceCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateServiceCommand { Name = GConst.ValidName, IndustryId = industryId }, CancellationToken.None);
            var ServiceId = context.Services.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateServiceCommandNotification>(c => c.ServiceId == ServiceId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}