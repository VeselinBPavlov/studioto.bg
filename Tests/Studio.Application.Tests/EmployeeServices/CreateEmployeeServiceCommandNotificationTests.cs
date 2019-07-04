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
        [Fact]
        public void ShouldRaiseEmployeeServiceCreatedNotification()
        {
            var employeeId = GetEmployeeId(null);
            var serviceId = GetServiceId(null);            

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateEmployeeServiceCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateEmployeeServiceCommand { Price = GConst.ValidPrice, EmployeeId = employeeId, ServiceId = serviceId }, CancellationToken.None);

            employeeId = context.EmployeeServices.SingleOrDefault(x => x.Price == GConst.ValidPrice).EmployeeId;
            serviceId = context.EmployeeServices.SingleOrDefault(x => x.Price == GConst.ValidPrice).ServiceId;


            mediatorMock.Verify(m => m.Publish(It.Is<CreateEmployeeServiceCommandNotification>(c => c.EmployeeId == employeeId && c.ServiceId == serviceId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}