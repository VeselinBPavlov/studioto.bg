namespace Studio.Application.Tests.Appointments.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAllAppointments;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllAppointmentsQueryHandlerTests : QueryTestFixture
    {
        private GetAllAppointmentsListQueryHandler sut;
        public GetAllAppointmentsQueryHandlerTests()
        {
            QueryArrangeHelper.AddAppointmentes(context);
            sut = new GetAllAppointmentsListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetAppointmentsTest()
        {
            var result = await sut.Handle(new GetAllAppointmentsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<AppointmentsListViewModel>();

            result.Appointments.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }       
}