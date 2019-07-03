namespace Studio.Application.Cities.Commands.Update
{
    using FluentValidation;

    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(c => c.Name).MaximumLength(100).NotEmpty();
        }
    }
}
