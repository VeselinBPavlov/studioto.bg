namespace Studio.Application.LocationIndustries.Commands.Update
{
    using FluentValidation;
    using Studio.Common;

    public class UpdateLocationIndustryCommandValidator : AbstractValidator<UpdateLocationIndustryCommand>
    {
        public const string Description = "Описание";

        public UpdateLocationIndustryCommandValidator()
        {
            RuleFor(es => es.LocationId).NotEqual(0);
            RuleFor(es => es.IndustryId).NotEqual(0);
            RuleFor(es => es.Description)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Description));
        }
    }
}
