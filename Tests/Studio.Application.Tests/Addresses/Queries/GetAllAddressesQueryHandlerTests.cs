namespace Studio.Application.Tests.Addresses.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Addresses.Queries.GetAllAddresses;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllAddressesQueryHandlerTests : QueryTestFixture
    {
        private GetAllAddressesListQueryHandler sut;
        public GetAllAddressesQueryHandlerTests()
        {
            QueryArrangeHelper.AddAddresses(context);
            sut = new GetAllAddressesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetAddressesTest()
        {
            var result = await sut.Handle(new GetAllAddressesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<AddressesListViewModel>();

            result.Addresses.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }       
}