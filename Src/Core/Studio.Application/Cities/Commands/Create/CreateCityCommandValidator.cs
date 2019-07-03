namespace Studio.Application.Cities.Commands.Create
{
    using FluentValidation;

    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(c => c.Name).MaximumLength(100).NotEmpty();
        }
    }
}
