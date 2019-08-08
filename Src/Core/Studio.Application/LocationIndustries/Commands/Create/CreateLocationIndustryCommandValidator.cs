namespace Studio.Application.LocationIndustries.Commands.Create
{
    using FluentValidation;
    using Studio.Common;

    public class CreateLocationIndustryCommandValidator : AbstractValidator<CreateLocationIndustryCommand>
    {
        public const string Description = "Описание";

        public CreateLocationIndustryCommandValidator()
        {
            RuleFor(es => es.LocationId).NotEqual(0);
            RuleFor(es => es.IndustryId).NotEqual(0);
            RuleFor(es => es.Description)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Description));
        }
    }
}
