namespace Studio.Application.Tests.Employees.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Studio.Application.Cities.Queries.GetEmployeesByLocation;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    public class GetEmployeesByLocationTests : QueryTestFixture
    {
        private GetEmployeesByLocationListQueryHandler sut;
        public GetEmployeesByLocationTests()
        {                        
            QueryArrangeHelper.AddEmployees(context);
            sut = new GetEmployeesByLocationListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetEmployeesByLocationTest()
        {
            var result = await sut.Handle(new GetEmployeesByLocationListQuery { LocationId = GConst.ValidId }, CancellationToken.None);

            result.ShouldBeOfType<EmployeesListViewModel>();
            // TODO: Test for empty collection.
        }
    }
}