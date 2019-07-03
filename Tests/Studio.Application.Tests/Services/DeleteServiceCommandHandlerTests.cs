namespace Studio.Application.Tests.Services.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Services.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteServiceCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteService()
        {
            var serviceId = GetServiceId(null);

            var sut = new DeleteServiceCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteServiceCommand { Id = serviceId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task ServiceShouldТhrowDeleteFailureException()
        {
            var serviceId = GetServiceId(null);

            GetAppointmentId(serviceId, null, null);

            var sut = new DeleteServiceCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteServiceCommand { Id = serviceId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Service, serviceId, GConst.Appointments, GConst.ServiceLower), status.Message);
        }

        [Fact]
        public async Task ServiceShouldТhrowDeleteFailureExceptionEmployee()
        {
            var serviceId = GetServiceId(null);

            var employeeId = GetEmployeeId(null);

            AddEmployeeService(serviceId, employeeId);

            var sut = new DeleteServiceCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteServiceCommand { Id = serviceId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Service, serviceId, GConst.Employees, GConst.ServiceLower), status.Message);
        }

        [Fact]
        public async Task ServiceShouldТhrowNotFoundException()
        {
            var sut = new DeleteServiceCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteServiceCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Service, GConst.InvalidId), status.Message);
        }
    }
}
