namespace Studio.Application.Addresses.Queries.GetAllNames
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
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddressesNamesListViewModel
    {
        public IList<AddressNameViewModel> Addresses { get; set; }          
    }

    public class AddressNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Address, AddressNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.AddressFormat.ToString()));
        }
    }

    public class GetAddressesNamesListQuery : IRequest<AddressesNamesListViewModel>
    {
    }

    public class GetAddressesNamesListQueryHandler : IRequestHandler<GetAddressesNamesListQuery, AddressesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAddressesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AddressesNamesListViewModel> Handle(GetAddressesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new AddressesNamesListViewModel
            {
                Addresses = await this.context.Addresses.Where(a => a.Location == null || a.IsDeleted != true).ProjectTo<AddressNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
