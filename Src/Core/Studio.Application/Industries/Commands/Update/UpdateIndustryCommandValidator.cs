namespace Studio.Application.Industries.Commands.Update
{
    using FluentValidation;
    using Studio.Common;

    public class UpdateIndustryCommandValidator : AbstractValidator<UpdateIndustryCommand>
    {
        private const string Name = "Име";
        private const string Possition = "Професия";

        public UpdateIndustryCommandValidator()
        {
            RuleFor(i => i.Name)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Name, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Name));
            RuleFor(i => i.Possition)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Possition, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Possition));
        }
    }
}
