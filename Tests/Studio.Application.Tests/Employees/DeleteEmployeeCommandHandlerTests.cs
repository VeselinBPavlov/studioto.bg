namespace Studio.Application.Tests.Employees.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Employees.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteEmployeeCommandHandlerTests : CommandTestBase
    {
        private int employeeId;
        private int serviceId;
        private DeleteEmployeeCommandHandler sut;

        public DeleteEmployeeCommandHandlerTests()
        {
            employeeId = GetEmployeeId(null);
            serviceId = GetServiceId(null);
            sut = new DeleteEmployeeCommandHandler(context);
        }

        [Fact]
        public async Task ShouldDeleteEmployee()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteEmployeeCommand { Id = employeeId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task EmployeeShouldТhrowDeleteFailureExceptionReferenceAppointments()
        {
            GetAppointmentId(null, employeeId, null);
                        
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteEmployeeCommand { Id = employeeId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Employee, employeeId, GConst.Appointments, GConst.EmployeeLower), status.Message);
        }

        [Fact]
        public async Task EmployeeShouldТhrowDeleteFailureExceptionReferenceServices()
        {
            AddEmployeeService(serviceId, employeeId);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteEmployeeCommand { Id = employeeId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Employee, employeeId, GConst.Services, GConst.EmployeeLower), status.Message);
        }

        [Fact]
        public async Task EmployeeShouldТhrowNotFoundException()
        {         
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteEmployeeCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Employee, GConst.InvalidId), status.Message);
        }
    }
}
