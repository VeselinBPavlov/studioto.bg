namespace Studio.Application.Tests.Appointments.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAllAppointments;
    using Studio.Application.Appointments.Queries.GetAppointmentsByUserId;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAppointmentsByUserIdQueryHandlerTests : QueryTestFixture
    {
        private GetAppointmentsByUserIdListQueryHandler sut;
        private string userId;

        public GetAppointmentsByUserIdQueryHandlerTests()
        {
            userId = QueryArrangeHelper.AddAppointmentes(context);
            sut = new GetAppointmentsByUserIdListQueryHandler(context, mapper);
        }

        //[Fact]
        //public async Task GetAppointmentsTest()
        //{
        //    var result = await sut.Handle(new GetAppointmentsByUserIdListQuery { UserId = userId }, CancellationToken.None);

        //    result.ShouldBeOfType<AppointmentsProfileListViewModel>();

        //    result.NewAppointments.Count.ShouldBe(GConst.ValidQueryCount);
        //    result.OldAppointments.Count.ShouldBe(GConst.ZeroId);
        //}
    }       
}