namespace Studio.Application.Cities.Commands.Create
{
    using FluentValidation;
    using Studio.Common;

    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public const string Name = "Име";
        public CreateCityCommandValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Name, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Name));
        }
    }
}
