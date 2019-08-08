namespace Studio.Application.Tests.Employees.Queries
{
    using Shouldly;
    using Studio.Application.Employees.Queries.GetAllEmployeeNames;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllEmployeesNames : QueryTestFixture
    {
        private readonly GetEmployeesNamesListQueryHandler sut;

        public GetAllEmployeesNames()
        {
            QueryArrangeHelper.AddEmployees(context);
            sut = new GetEmployeesNamesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetEmployeesNamesTest()
        {
            var result = await sut.Handle(new GetEmployeesNamesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<EmployeesNamesListViewModel>();

            result.Employees.Count.ShouldBe(GConst.ValidCount);
        }
    }
}
