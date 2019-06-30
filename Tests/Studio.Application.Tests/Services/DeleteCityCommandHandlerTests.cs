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
            var service = new Service { Name = GlobalConstants.ServiceValidName };

            context.Services.Add(service);
            await context.SaveChangesAsync();

            var serviceId = context.Services.SingleOrDefault(x => x.Name == GlobalConstants.ServiceValidName).Id;

            var sut = new DeleteServiceCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteServiceCommand { Id = serviceId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task ServiceShouldТhrowDeleteFailureException()
        {
            var service = new Service { Name = "Beauty" };

            context.Services.Add(service);
            await context.SaveChangesAsync();

            var serviceId = context.Services.SingleOrDefault(x => x.Name == "Beauty").Id;

            var appointment = new Appointment { ServiceId = serviceId };
            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            var sut = new DeleteServiceCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteServiceCommand { Id = serviceId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.DeleteFailureExceptionMessage, nameof(Service), serviceId, "appointments", "service"), status.Message);
        }

        [Fact]
        public async Task ServiceShouldТhrowDeleteFailureExceptionEmployee()
        {
            var service = new Service { Name = "Beauty" };

            context.Services.Add(service);
            await context.SaveChangesAsync();

            var serviceId = context.Services.SingleOrDefault(x => x.Name == "Beauty").Id;

            var employeeService = new EmployeeService { ServiceId = serviceId, EmployeeId = 1 };
            context.EmployeeServices.Add(employeeService);
            await context.SaveChangesAsync();

            var sut = new DeleteServiceCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteServiceCommand { Id = serviceId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.DeleteFailureExceptionMessage, nameof(Service), serviceId, "employees", "service"), status.Message);
        }

        [Fact]
        public async Task ServiceShouldТhrowNotFoundException()
        {
            var sut = new DeleteServiceCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteServiceCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.NotFoundExceptionMessage, nameof(Service), GlobalConstants.InvalidId), status.Message);
        }
    }
}
