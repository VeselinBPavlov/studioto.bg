namespace Studio.Application.LocationIndustries.Commands.Create
{
    using FluentValidation;

    public class CreateLocationIndustryCommandValidator : AbstractValidator<CreateLocationIndustryCommand>
    {
        public CreateLocationIndustryCommandValidator()
        {
            RuleFor(es => es.LocationId).NotEqual(0);
            RuleFor(es => es.IndustryId).NotEqual(0);
            RuleFor(es => es.Description).NotEmpty();
        }
    }
}
