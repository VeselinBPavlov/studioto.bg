namespace Studio.Application.Countries.Commands.Update
{
    using FluentValidation;

    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(c => c.Name).MaximumLength(100).NotEmpty();
        }
    }
}
