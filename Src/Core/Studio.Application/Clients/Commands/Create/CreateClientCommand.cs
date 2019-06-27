namespace Studio.Application.Clients.Commands.Create
{
    using MediatR;
    using System;

    public class CreateClientCommand : IRequest
    {
        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string Phone { get; set; }

    }
}
