namespace Studio.Application.Addresses.Queries.GetAddressById
{
    using AutoMapper;
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressViewModel>
    {
        private readonly IStudioDbContext context;

        public GetAddressByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }
        public async Task<AddressViewModel> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await context.Addresses.FindAsync(request.Id);

            if (address == null)
            {
                throw new NotFoundException(GConst.Address, request.Id);
            }

            return AddressViewModel.Create(address);
        }
    }
}