namespace Studio.Application.Tests.EmployeeServices.Queries
{
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAppointmentById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Studio.Application.Cities.Queries.GetEmployeeServiceById;

    public class GetEmployeeServiceByIdTests : QueryTestFixture
    {
        private GetEmployeeServiceByIdQueryHandler sut;

        public GetEmployeeServiceByIdTests()
        {
            QueryArrangeHelper.AddEmployeeServices(context);
            sut = new GetEmployeeServiceByIdQueryHandler(context);
        }

        // [Fact]
        // public async Task GetEmployeeServiceByIdTest()
        // {
        //     var status = await sut.Handle(new GetEmployeeServiceByIdQuery { EmployeeId = GConst.ValidId, ServiceId = GConst.ValidId }, CancellationToken.None);

        //     status.ShouldBeOfType<EmployeeServiceViewModel>();
        //     status.EmployeeId.ShouldBe(GConst.ValidId);
        //     status.ServiceId.ShouldBe(GConst.ValidId);
        // }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetEmployeeServiceByIdQuery { EmployeeId = GConst.InvalidId, ServiceId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.EmployeeService, GConst.InvalidId + "/" + GConst.InvalidId), status.Message);
        }
    }
}