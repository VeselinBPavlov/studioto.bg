namespace Studio.Application.Cities.Commands.Update
{
    using FluentValidation;
    using Studio.Common;

    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public const string Name = "Име";
        
        public UpdateCityCommandValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Name, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Name));
        }
    }
}
