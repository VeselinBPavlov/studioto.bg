namespace Studio.Application.Tests.EmployeeServices.Commands
{
    using MediatR;
    using Studio.Application.EmployeeServices.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateEmployeeServiceCommandHandlerTests : CommandTestBase
    {
        private int employeeId;
        private int serviceId;
        private UpdateEmployeeServiceCommandHandler sut;

        public UpdateEmployeeServiceCommandHandlerTests()
        {
            employeeId = CommandArrangeHelper.GetEmployeeId(context, null);
            serviceId = CommandArrangeHelper.GetServiceId(context, null);
            sut = new UpdateEmployeeServiceCommandHandler(context);
        }

        [Fact]
        public async void EmployeeServiceShouldUpdateCorrect()
        {
            CommandArrangeHelper.AddEmployeeService(context, serviceId, employeeId);

            var updatedEmployeeService = new UpdateEmployeeServiceCommand { EmployeeId = employeeId, ServiceId = serviceId, Price = GConst.ValidPrice };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedEmployeeService, CancellationToken.None));

            var employeeIdResult = context.EmployeeServices.SingleOrDefault(x => x.Price == GConst.ValidPrice).EmployeeId;
            var serviceIdResult = context.EmployeeServices.SingleOrDefault(x => x.Price == GConst.ValidPrice).ServiceId;

            Assert.Equal(employeeId, employeeIdResult);
            Assert.Equal(serviceId, serviceIdResult);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.EmployeeServices.Count());
        }

        [Fact]
        public async void EmployeeServiceShouldThrowNotFoundException()
        {
            var updatedEmployeeService = new UpdateEmployeeServiceCommand { EmployeeId = GConst.InvalidId, ServiceId = GConst.InvalidId, Price = GConst.ValidPrice };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedEmployeeService, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.EmployeeService, $"{GConst.InvalidId} - {GConst.InvalidId}"), status.Message);            
        }
    }
}
