namespace Studio.Application.Tests.EmployeeServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.EmployeeServices.Queries.GetAllEmployeeServices;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllEmployeeServicesQueryHandlerTests : QueryTestFixture
    {
        private GetAllEmployeeServicesListQueryHandler sut;
        public GetAllEmployeeServicesQueryHandlerTests()
        {
            QueryArrangeHelper.AddEmployeeServices(context);
            sut = new GetAllEmployeeServicesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetEmployeeServicesTest()
        {
            var result = await sut.Handle(new GetAllEmployeeServicesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<EmployeeServicesListViewModel>();

            result.EmployeeServices.Count.ShouldBe(GConst.ValidCount);
        }
    }       
}