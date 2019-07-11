namespace Studio.Application.Tests.Employees.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Studio.Application.Cities.Queries.GetCityById;
    using Studio.Application.Cities.Queries.GetEmployeesByLocation;
    using Studio.Application.Employees.Queries.GetPageEmployeeById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
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
        public async Task GetEmployeesByLocationTest()
        {
            var status = await sut.Handle(new GetEmployeeByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<EmployeeProfileViewModel>();
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