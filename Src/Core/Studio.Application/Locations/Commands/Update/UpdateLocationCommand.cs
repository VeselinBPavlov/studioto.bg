namespace Studio.Application.Locations.Commands.Update
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class UpdateLocationCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

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
