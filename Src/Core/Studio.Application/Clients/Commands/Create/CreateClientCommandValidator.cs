namespace Studio.Application.Industries.Commands.Create
{
    using FluentValidation;

    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(c => c.CompanyName).MaximumLength(100).NotEmpty();
            RuleFor(c => c.VatNumber).Matches(@"^(BG)|\d{9}$").NotEmpty();
            RuleFor(c => c.Phone).Matches(@"^(\+359|0)(\d{9})$").NotEmpty();
            RuleFor(c => c.ManagerFirstName).MaximumLength(50).NotEmpty();
            RuleFor(c => c.ManagerLastName).MaximumLength(50).NotEmpty();            
        }
    }
}
