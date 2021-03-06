namespace Studio.Application.Tests.Employees.Queries
{
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAppointmentById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Employees.Queries.GetEmployeeById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetEmployeeByIdTests : QueryTestFixture
    {
        private GetEmployeeByIdQueryHandler sut;

        public GetEmployeeByIdTests()
        {
            QueryArrangeHelper.AddEmployees(context);
            sut = new GetEmployeeByIdQueryHandler(context);
        }

        [Fact]
        public async Task GetEmployeeByIdTest()
        {
            var status = await sut.Handle(new GetEmployeeByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<EmployeeViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetEmployeeByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Employee, GConst.InvalidId), status.Message);
        }
    }
}