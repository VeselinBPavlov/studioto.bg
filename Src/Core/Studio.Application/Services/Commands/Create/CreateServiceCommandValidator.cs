namespace Studio.Application.Services.Commands.Create
{
    using FluentValidation;
    using Studio.Common;

    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        private const string Name = "Услуга";

        public CreateServiceCommandValidator()
        {
            RuleFor(s => s.Name)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Name, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Name));
        }
    }
}
