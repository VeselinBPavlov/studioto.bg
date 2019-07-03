namespace Studio.Application.Tests.Employees.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Employees.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateEmployeeCommandNotificationTests : CommandTestBase
    {
        [Fact]
        public void ShouldRaiseEmployeeCreatedNotification()
        {
            var locationId = GetLocationId(null, null);

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateEmployeeCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateEmployeeCommand { FirstName = GConst.ValidName, LastName = GConst.ValidName, LocationId = locationId }, CancellationToken.None);

            var employeeId = context.Employees.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateEmployeeCommandNotification>(c => c.EmployeeId == employeeId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}