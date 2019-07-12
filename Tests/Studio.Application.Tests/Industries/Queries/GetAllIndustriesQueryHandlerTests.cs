namespace Studio.Application.Tests.Industries.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Industries.Queries.GetAllIndustries;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllIndustriesQueryHandlerTests : QueryTestFixture
    {
        private GetAllIndustriesListQueryHandler sut;
        public GetAllIndustriesQueryHandlerTests()
        {
            QueryArrangeHelper.AddIndustries(context);
            sut = new GetAllIndustriesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetIndustriesTest()
        {
            var result = await sut.Handle(new GetAllIndustriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<IndustriesListViewModel>();

            result.Industries.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }       
}