namespace Studio.Application.LocationIndustries.Commands.Update
{
    using FluentValidation;

    public class UpdateLocationIndustryCommandValidator : AbstractValidator<UpdateLocationIndustryCommand>
    {
        public UpdateLocationIndustryCommandValidator()
        {
            RuleFor(es => es.LocationId).NotEqual(0);
            RuleFor(es => es.IndustryId).NotEqual(0);
            RuleFor(es => es.Description).NotEmpty();
        }
    }
}
