namespace Studio.Application.Tests.Addresses.Queries
{
    using Shouldly;
    using Studio.Application.Addresses.Queries.GetAddressById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetAddressByIdTests : QueryTestFixture
    {
        private GetAddressByIdQueryHandler sut;

        public GetAddressByIdTests()
        {
            QueryArrangeHelper.AddAddresses(context);
            sut = new GetAddressByIdQueryHandler(context);
        }

        [Fact]
        public async Task GetAddressByIdTest()
        {
            var status = await sut.Handle(new GetAddressByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<AddressViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetAddressByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Address, GConst.InvalidId), status.Message);
        }
    }
}