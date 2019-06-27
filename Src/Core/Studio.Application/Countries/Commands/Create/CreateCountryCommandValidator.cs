namespace Studio.Application.Countries.Commands.Create
{
    using FluentValidation;
    using Studio.Application.Countries.Commands.Create;

    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(i => i.Name).MaximumLength(100).NotEmpty();
        }
    }
}
