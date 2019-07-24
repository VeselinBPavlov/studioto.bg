namespace Studio.Application.Tests.Addresses.Queries
{
    using Shouldly;
    using Studio.Application.Addresses.Queries.GetAllNames;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllAddressesNamesQueryHandlerTests : QueryTestFixture
    {
        private GetAddressesNamesListQueryHandler sut;

        public GetAllAddressesNamesQueryHandlerTests()
        {
            QueryArrangeHelper.AddAddresses(context);
            sut = new GetAddressesNamesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetAddressesNamesTest()
        {
            var result = await sut.Handle(new GetAddressesNamesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<AddressesNamesListViewModel>();

            result.Addresses.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }
}
