namespace Studio.Application.Addresses.Commands.Update
{
    using MediatR;

    public class UpdateAddressCommand : IRequest
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public string District { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }
    }
}
