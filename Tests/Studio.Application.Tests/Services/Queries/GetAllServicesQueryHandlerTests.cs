namespace Studio.Application.Tests.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Services.Queries.GetAllServices;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllServicesQueryHandlerTests : QueryTestFixture
    {
        private GetAllServicesListQueryHandler sut;
        public GetAllServicesQueryHandlerTests()
        {
            QueryArrangeHelper.AddServices(context);
            sut = new GetAllServicesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetServicesTest()
        {
            var result = await sut.Handle(new GetAllServicesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ServicesListViewModel>();

            result.Services.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }       
}