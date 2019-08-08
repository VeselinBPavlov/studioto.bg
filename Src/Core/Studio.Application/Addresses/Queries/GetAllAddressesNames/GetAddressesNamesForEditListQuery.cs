namespace Studio.Application.Addresses.Queries.GetAllAddressesNames
{
    using MediatR;

    public class GetAddressesNamesForEditListQuery : IRequest<AddressesNamesListViewModel>
    {
        public int LocationId { get; set; }
    }
}
