namespace Studio.Application.Tests.EmployeeServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Studio.Application.EmployeeServices.Queries.GetServicesByEmployeeId;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetServicesByEmployeeIdTests : QueryTestFixture
    {
        private GetServicesByEmployeeIdListQueryHandler sut;
        public GetServicesByEmployeeIdTests()
        {
            QueryArrangeHelper.AddEmployeeServices(context);
            sut = new GetServicesByEmployeeIdListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetEmployeeServicesTest()
        {
            var result = await sut.Handle(new GetServicesByEmployeeIdListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ServicesByEmployeeIdListViewModel>();

            result.Services.Count.ShouldBe(GConst.ValidCount);
        }
    }       
}