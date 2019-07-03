namespace Studio.Application.ContactForms.Commands.Create
{
    using MediatR;

    public class CreateContactFormCommand : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Topic { get; set; }         

        public string Message { get; set; }
    }
}
