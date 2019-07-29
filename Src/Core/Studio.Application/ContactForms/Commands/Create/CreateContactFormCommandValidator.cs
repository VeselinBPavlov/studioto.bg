namespace Studio.Application.ContactForms.Commands.Create
{
    using FluentValidation;
    using Studio.Common;

    public class CreateContactFormCommandValidator : AbstractValidator<CreateContactFormCommand>
    {
        private const string FirstName = "Име";
        private const string LastName = "Фамилия";
        private const string Email = "Email";
        private const string Topic = "Тема";
        private const string Message = "Съобщение";

        public CreateContactFormCommandValidator()
        {
            RuleFor(cf => cf.FirstName)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, FirstName, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, FirstName));
            RuleFor(cf => cf.LastName)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, LastName, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, LastName));
            RuleFor(cf => cf.Email)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Email, 1, 100))
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage(GConst.ErrorEmailMessage)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Email));
            RuleFor(cf => cf.Topic)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Topic, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Topic));
            RuleFor(cf => cf.Message)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Message));
        }
    }
}
