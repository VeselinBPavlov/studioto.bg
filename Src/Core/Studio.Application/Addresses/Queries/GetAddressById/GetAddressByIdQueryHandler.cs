namespace Studio.Application.Addresses.Queries.GetAddressById
{
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Common;

    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressViewModel>
    {
        private readonly IStudioDbContext context;

        public GetAddressByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<AddressViewModel> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await this.context.Addresses.Include(c => c.AddressFormat).Include(a => a.City).SingleOrDefaultAsync(c => c.Id == request.Id);

            if (address == null)
            {
                throw new NotFoundException(GConst.Address, request.Id);
            }

            return AddressViewModel.Create(address);
        }
    }
}