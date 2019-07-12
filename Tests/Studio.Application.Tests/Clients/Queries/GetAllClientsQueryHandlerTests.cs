namespace Studio.Application.Tests.Clients.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Clients.Queries.GetAllClients;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllClientsQueryHandlerTests : QueryTestFixture
    {
        private GetAllClientsListQueryHandler sut;
        public GetAllClientsQueryHandlerTests()
        {
            QueryArrangeHelper.AddClients(context);
            sut = new GetAllClientsListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetClientsTest()
        {
            var result = await sut.Handle(new GetAllClientsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ClientsListViewModel>();

            result.Clients.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }       
}