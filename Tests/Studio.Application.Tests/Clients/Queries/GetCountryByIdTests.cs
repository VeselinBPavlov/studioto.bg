namespace Studio.Application.Tests.Clients.Queries
{
    using Shouldly;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetClientByIdTests : QueryTestFixture
    {
        private GetClientByIdQueryHandler sut;

        public GetClientByIdTests()
        {
            QueryArrangeHelper.AddClients(context);
            sut = new GetClientByIdQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetClientsByIdTest()
        {
            var status = await sut.Handle(new GetClientByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<ClientViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetClientByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Client, GConst.InvalidId), status.Message);
        }
    }
}