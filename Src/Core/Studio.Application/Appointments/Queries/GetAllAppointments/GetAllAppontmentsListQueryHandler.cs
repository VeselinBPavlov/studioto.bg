namespace Studio.Application.Appointments.Queries.GetAllAppointments
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllAppointmentsListQueryHandler : IRequestHandler<GetAllAppointmentsListQuery, AppointmentsListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllAppointmentsListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<AppointmentsListViewModel> Handle(GetAllAppointmentsListQuery request, CancellationToken cancellationToken)
        {
            return new AppointmentsListViewModel
            {
                Appointments = await this.context.Appointments.ProjectTo<AppointmentAllViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}