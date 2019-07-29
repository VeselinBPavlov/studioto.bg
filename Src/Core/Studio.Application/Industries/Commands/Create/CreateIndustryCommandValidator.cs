namespace Studio.Application.Industries.Commands.Create
{
    using FluentValidation;
    using Studio.Common;

    public class CreateIndustryCommandValidator : AbstractValidator<CreateIndustryCommand>
    {
        private const string Name = "Име";
        private const string Possition = "Професия";
        public CreateIndustryCommandValidator()
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
