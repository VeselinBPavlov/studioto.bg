namespace Studio.Application.Appointments.Queries.GetAppointmentById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Countries.Queries.GetCountryById;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentViewModel>
    {
        private readonly IStudioDbContext context;

        public GetAppointmentByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<AppointmentViewModel> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await this.context.Appointments.FindAsync(request.Id);

            if (appointment == null)
            {
                throw new NotFoundException(GConst.Appointment, request.Id);
            }

            return AppointmentViewModel.Create(appointment);
        }
    }
}