namespace Studio.Application.Tests.Clients.Queries
{
    using Shouldly;
    using Studio.Application.Clients.Queries.GetAllClientsNames;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllClientsNames : QueryTestFixture
    {
        private readonly GetClientsNamesListQueryHandler sut;

        public GetAllClientsNames()
        {
            QueryArrangeHelper.AddClients(context);
            sut = new GetClientsNamesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetClientsNamesTest()
        {
            var result = await sut.Handle(new GetClientsNamesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ClientsNamesListViewModel>();

            result.Clients.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }
}
