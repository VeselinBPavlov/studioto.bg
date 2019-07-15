namespace Studio.Application.Tests.LocationIndustries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.LocationIndustries.Queries.GetAllLocationIndustries;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllLocationIndustriesQueryHandlerTests : QueryTestFixture
    {
        private GetAllLocationIndustriesListQueryHandler sut;
        public GetAllLocationIndustriesQueryHandlerTests()
        {
            QueryArrangeHelper.AddLocationIndustries(context);
            sut = new GetAllLocationIndustriesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetLocationIndustriesTest()
        {
            var result = await sut.Handle(new GetAllLocationIndustriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<LocationIndustriesListViewModel>();

            result.LocationIndustries.Count.ShouldBe(GConst.ValidCount);
        }
    }       
}