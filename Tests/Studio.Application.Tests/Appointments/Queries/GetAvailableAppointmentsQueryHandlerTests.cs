namespace Studio.Application.Tests.Appointments.Queries
{
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAppointmentById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Studio.Application.Appointments.Commands.Create;
    using System;
    using Studio.Application.Appointments.Queries.GetAvailableAppointments;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GetAvailableAppointmentsQueryHandlerTests : QueryTestFixture
    {
        private GetAvailableAppontmentsQueryHandler sut;
        private CreateAppointmentCommand command;
        private string userId;
        private int employeeId;
        private int serviceId;     
        private int[] ids; 

        public GetAvailableAppointmentsQueryHandlerTests()
        {
            ids = CommandArrangeHelper.GetEmployeeServiceIds(context);
            employeeId = ids[0];
            serviceId = ids[1];
            userId = CommandArrangeHelper.GetUserId(context);
            sut = new GetAvailableAppontmentsQueryHandler(context);
            command = new CreateAppointmentCommand { UserId = userId, ServiceId = serviceId, EmployeeId = employeeId, ReservationDate = new DateTime(2019, 12, 2), TimeBlockHelper = "10:00" };
        }

        // [Fact]
        // public async Task GetAvailableAppointmentsTest()
        // {
        //     var status = await sut.Handle(new GetAvailableAppointmentsQuery { Command = command }, CancellationToken.None);

        //     status.ShouldBeOfType<AvailableAppointmentsViewModel>();
        //     status.AvailableAppointments.ShouldBeOfType<List<SelectListItem>>();
        //     status.AvailableAppointments.Count.ShouldBe(GConst.AvailableAppointmentsCount);
        // }

        // [Fact]
        // public async Task ShouldReturnNoFreeHours()
        // {
        //     command.ReservationDate = DateTime.UtcNow.AddDays(-1);
        //     var status = await sut.Handle(new GetAvailableAppointmentsQuery { Command = command }, CancellationToken.None);

        //     status.ShouldBeOfType<AvailableAppointmentsViewModel>();
        //     status.AvailableAppointments.ShouldBeOfType<List<SelectListItem>>();
        //     status.AvailableAppointments.Count.ShouldBe(GConst.ValidCount);
        //     status.AvailableAppointments[0].Value.ShouldBe(GConst.AllHoursBusy);
        // }
    }
}