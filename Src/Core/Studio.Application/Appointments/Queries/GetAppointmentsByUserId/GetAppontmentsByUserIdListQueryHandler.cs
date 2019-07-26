namespace Studio.Application.Appointments.Queries.GetAppointmentsByUserId
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAppointmentsByUserIdListQueryHandler : IRequestHandler<GetAppointmentsByUserIdListQuery, AppointmentsProfileListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAppointmentsByUserIdListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AppointmentsProfileListViewModel> Handle(GetAppointmentsByUserIdListQuery request, CancellationToken cancellationToken)
        {
            return new AppointmentsProfileListViewModel 
            {
                NewAppointments = await this.context.Appointments.Where(a => a.UserId == request.UserId && a.IsDeleted != true && a.ReservationDate >= DateTime.UtcNow.Date && a.ReservationTime >= DateTime.UtcNow).ProjectTo<AppointmentProfileViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                OldAppointments = await this.context.Appointments.Where(a => a.UserId == request.UserId && a.IsDeleted != true && a.ReservationDate < DateTime.UtcNow.Date && a.ReservationTime < DateTime.UtcNow).ProjectTo<AppointmentProfileViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}