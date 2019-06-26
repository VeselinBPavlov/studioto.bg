namespace Studio.Application.Industries.Commands.Create
{
    using FluentValidation;

    public class CreateIndustryCommandValidator : AbstractValidator<CreateIndustryCommand>
    {
        public CreateIndustryCommandValidator()
        {
            RuleFor(i => i.Name).MaximumLength(100).NotEmpty();
            RuleFor(i => i.Possition).MaximumLength(100).NotEmpty();
        }
    }
}
