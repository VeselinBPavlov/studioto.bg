namespace Studio.Application.Clients.Commands.Update
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class UpdateClientCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string Phone { get; set; }
    }
}
