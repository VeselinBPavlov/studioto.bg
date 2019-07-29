namespace Studio.Application.Countries.Commands.Create
{
    using FluentValidation;
    using Studio.Application.Countries.Commands.Create;
    using Studio.Common;

    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        private const string Name = "Държава";

        public CreateCountryCommandValidator()
        {            
            RuleFor(c => c.Name)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Name, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Name));
        }
    }
}
