namespace Studio.Application.Tests.Employees.Commands
{
    using MediatR;
    using Studio.Application.Employees.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateEmployeeCommandHandlerTests : CommandTestBase
    {
        private int locationId;
        private int employeeId;
        private UpdateEmployeeCommandHandler sut;

        public UpdateEmployeeCommandHandlerTests()
        {
            locationId = CommandArrangeHelper.GetLocationId(context, null, null);
            employeeId = CommandArrangeHelper.GetEmployeeId(context, locationId);
            sut = new UpdateEmployeeCommandHandler(context);
        }

        [Fact]
        public async void EmployeeShouldUpdateCorrect()
        {
            var updatedEmployee = new UpdateEmployeeCommand { Id = employeeId, FirstName = GConst.ValidName, LocationId = locationId };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedEmployee, CancellationToken.None));

            var resultId = context.Employees.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            Assert.Equal(employeeId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Employees.Count());
        }

        [Fact]
        public async void EmployeeShouldThrowReferenceException()
        {
            var updatedEmployee = new UpdateEmployeeCommand { Id = employeeId, FirstName = GConst.ValidName, LocationId = GConst.InvalidId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedEmployee, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Employee, employeeId, GConst.LocationLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void EmployeeShouldThrowNotFoundException()
        {
            var updatedEmployee = new UpdateEmployeeCommand { Id = GConst.InvalidId, FirstName = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedEmployee, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Employee, GConst.InvalidId), status.Message);            
        }
    }
}
