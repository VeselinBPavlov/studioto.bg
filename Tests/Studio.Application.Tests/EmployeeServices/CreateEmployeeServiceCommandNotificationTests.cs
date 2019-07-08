namespace Studio.Application.Tests.EmployeeServices.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.EmployeeServices.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateEmployeeServiceCommandNotificationTests : CommandTestBase
    {
        private int employeeId;
        private int serviceId;
        private Mock<IMediator> mediator;
        private CreateEmployeeServiceCommandHandler sut;

        public CreateEmployeeServiceCommandNotificationTests()
        {
            employeeId = ArrangeHelper.GetEmployeeId(context, null);
            serviceId = ArrangeHelper.GetServiceId(context, null);
            mediator = new Mock<IMediator>();
            sut = new CreateEmployeeServiceCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseEmployeeServiceCreatedNotification()
        {
            var result = sut.Handle(new CreateEmployeeServiceCommand { Price = GConst.ValidPrice, EmployeeId = employeeId, ServiceId = serviceId }, CancellationToken.None);

            employeeId = context.EmployeeServices.SingleOrDefault(x => x.Price == GConst.ValidPrice).EmployeeId;
            serviceId = context.EmployeeServices.SingleOrDefault(x => x.Price == GConst.ValidPrice).ServiceId;


            mediator.Verify(m => m.Publish(It.Is<CreateEmployeeServiceCommandNotification>(c => c.EmployeeId == employeeId && c.ServiceId == serviceId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}