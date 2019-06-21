namespace Studio.Application.Industries.Commands.Update
{
    using FluentValidation;

    public class UpdateIndustryCommandValidator : AbstractValidator<UpdateIndustryCommand>
    {
        public UpdateIndustryCommandValidator()
        {
            RuleFor(i => i.Name).MaximumLength(100).NotEmpty();
        }
    }
}
