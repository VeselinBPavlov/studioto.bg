namespace Studio.Application.Services.Commands.Create
{
    using FluentValidation;

    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator()
        {
            RuleFor(i => i.Name).MaximumLength(100).NotEmpty();
        }
    }
}
