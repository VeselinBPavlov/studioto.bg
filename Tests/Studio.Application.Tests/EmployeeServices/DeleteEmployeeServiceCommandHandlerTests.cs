namespace Studio.Application.Tests.EmployeeServices.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.EmployeeServices.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteEmployeeServiceCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteEmployeeService()
        {
            var employeeId = GetEmployeeId(null);
            var serviceId = GetServiceId(null);

            AddEmployeeService(serviceId, employeeId);

            var sut = new DeleteEmployeeServiceCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteEmployeeServiceCommand { EmployeeId = employeeId, ServiceId = serviceId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task EmployeeServiceShouldТhrowNotFoundException()
        {
            var sut = new DeleteEmployeeServiceCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteEmployeeServiceCommand { EmployeeId = GConst.InvalidId, ServiceId = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.EmployeeService, $"{GConst.InvalidId} - {GConst.InvalidId}"), status.Message);
        }
    }
}
