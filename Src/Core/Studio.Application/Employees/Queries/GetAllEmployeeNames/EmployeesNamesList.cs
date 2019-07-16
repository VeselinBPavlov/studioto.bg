namespace Studio.Application.Employees.Queries.GetAllNames
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmployeesNamesListViewModel
    {
        public IList<EmployeeNameViewModel> Employees { get; set; }      
    }

    public class EmployeeNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Employee, EmployeeNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }

    public class GetEmployeesNamesListQuery : IRequest<EmployeesNamesListViewModel>
    {
    }

    public class GetEmployeesNamesListQueryHandler : IRequestHandler<GetEmployeesNamesListQuery, EmployeesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetEmployeesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EmployeesNamesListViewModel> Handle(GetEmployeesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new EmployeesNamesListViewModel
            {
                Employees = await this.context.Employees.Where(c => c.IsDeleted != true).ProjectTo<EmployeeNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
