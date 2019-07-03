namespace Studio.Application.ContactForms.Commands.Create
{
    using FluentValidation;

    public class CreateContactFormCommandValidator : AbstractValidator<CreateContactFormCommand>
    {
        public CreateContactFormCommandValidator()
        {
            RuleFor(cf => cf.FirstName).MaximumLength(100).NotEmpty();
            RuleFor(cf => cf.LastName).MaximumLength(100).NotEmpty();
            RuleFor(cf => cf.Email).MaximumLength(100).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").NotEmpty();
            RuleFor(cf => cf.Topic).MaximumLength(100).NotEmpty();
            RuleFor(cf => cf.Message).NotEmpty();
        }
    }
}
