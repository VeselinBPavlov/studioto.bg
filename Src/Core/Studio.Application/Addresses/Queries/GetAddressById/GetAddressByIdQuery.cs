namespace Studio.Application.Addresses.Queries.GetAddressById
{
    using MediatR;

    public class GetAddressByIdQuery : IRequest<AddressViewModel>
    {
        public int Id { get; set; }
    }
}