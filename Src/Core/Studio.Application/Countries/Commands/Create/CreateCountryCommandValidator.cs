namespace Studio.Application.Countries.Commands.Create
{
    using FluentValidation;
    using Studio.Application.Countries.Commands.Create;

    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(c => c.Name).MaximumLength(100).NotEmpty();
        }
    }
}
