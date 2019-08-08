namespace Studio.Application.Tests.Industries.Queries
{
    using Shouldly;
    using Studio.Application.Industries.Queries.GetAllIndustriesNames;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllIndustriesNames : QueryTestFixture
    {
        private readonly GetIndustriesNamesListQueryHandler sut;

        public GetAllIndustriesNames()
        {
            QueryArrangeHelper.AddIndustries(context);
            sut = new GetIndustriesNamesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetIndustriesNamesTest()
        {
            var result = await sut.Handle(new GetIndustriesNamesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<IndustriesNamesListViewModel>();

            result.Industries.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }
}
