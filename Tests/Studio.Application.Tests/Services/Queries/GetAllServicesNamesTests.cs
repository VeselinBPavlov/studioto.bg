namespace Studio.Application.Tests.Services.Queries
{
    using Shouldly;
    using Studio.Application.Services.Queries.GetAllServicesNames;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllServicesNames : QueryTestFixture
    {
        private readonly GetServicesNamesListQueryHandler sut;

        public GetAllServicesNames()
        {
            QueryArrangeHelper.AddServices(context);
            sut = new GetServicesNamesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetServicesNamesTest()
        {
            var result = await sut.Handle(new GetServicesNamesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ServicesNamesListViewModel>();

            result.Services.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }
}
