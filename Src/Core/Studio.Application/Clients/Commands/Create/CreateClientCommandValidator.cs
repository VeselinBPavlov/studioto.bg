namespace Studio.Application.Clients.Commands.Create
{
    using FluentValidation;
    using Studio.Application.Clients.Commands.Create;
    using Studio.Common;

    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        private const string CompanyName = "Фирма";
        private const string VatNumber = "Данъчен номер";
        private const string Phone = "Телефон";
        private const string ManagerFirstName = "Име на управител";
        private const string ManagerLastName = "Фамилия на управител";

        public CreateClientCommandValidator()
        {            
            RuleFor(c => c.CompanyName)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, CompanyName, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, CompanyName));
            RuleFor(c => c.VatNumber)
                .Matches(@"^(BG)|\d{9}$")
                .WithMessage(GConst.ErrorVatNumberMessage)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, VatNumber));
            RuleFor(c => c.Phone)
                .Matches(@"^(\+359|0)(\d{9})$")
                .WithMessage(GConst.ErrorPhoneMessage)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Phone));
            RuleFor(c => c.ManagerFirstName)
                .MaximumLength(50)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, ManagerFirstName, 1, 50))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, ManagerFirstName));
            RuleFor(c => c.ManagerLastName)
                .MaximumLength(50)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, ManagerLastName, 1, 50))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, ManagerLastName));
        }
    }
}
