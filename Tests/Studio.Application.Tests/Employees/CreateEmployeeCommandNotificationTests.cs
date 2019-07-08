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
        private int locationId;
        private Mock<IMediator> mediator;
        private CreateEmployeeCommandHandler sut;

        public CreateEmployeeCommandNotificationTests()
        {
            locationId = ArrangeHelper.GetLocationId(context, null, null);
            mediator = new Mock<IMediator>();
            sut = new CreateEmployeeCommandHandler(context, mediator.Object);
        }

        [Fact]
        public void ShouldRaiseEmployeeCreatedNotification()
        {
            var result = sut.Handle(new CreateEmployeeCommand { FirstName = GConst.ValidName, LastName = GConst.ValidName, LocationId = locationId }, CancellationToken.None);

            var employeeId = context.Employees.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateEmployeeCommandNotification>(c => c.EmployeeId == employeeId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}