namespace Studio.Application.Tests.Employees
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Employees.Queries.GetAllEmployees;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllEmployeesQueryHandlerTests : QueryTestFixture
    {
        private GetAllEmployeesListQueryHandler sut;
        public GetAllEmployeesQueryHandlerTests()
        {
            QueryArrangeHelper.AddEmployees(context);
            sut = new GetAllEmployeesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetEmployeesTest()
        {
            var result = await sut.Handle(new GetAllEmployeesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<EmployeesListViewModel>();

            result.Employees.Count.ShouldBe(GConst.ValidCount);
        }
    }       
}