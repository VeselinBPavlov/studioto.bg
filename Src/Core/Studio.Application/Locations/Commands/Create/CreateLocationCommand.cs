namespace Studio.Application.Locations.Commands.Create
{
    using MediatR;
    using Studio.Domain.Enumerations;

    public class CreateLocationCommand : IRequest
    {
        public string Name { get; set; }

        public bool IsOffice { get; set; }

        public string StartDay { get; set; }

        public string EndDay { get; set; }

        public string StartHour { get; set; }

        public string EndHour { get; set; }

        public int ClientId { get; set; }

        public int AddressId { get; set; }
    }
}
