namespace Studio.Application.Countries.Commands.Update
{
    using FluentValidation;
    using Studio.Common;

    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        private const string Name = "Държава";

        public UpdateCountryCommandValidator()
        {            
            RuleFor(c => c.Name)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Name, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Name));
        }
    }
}
