namespace Studio.Application.Industries.Commands.Update
{
    using MediatR;

    public class UpdateClientCommand : IRequest
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }
        
        public string Phone { get; set; }
    }
}
