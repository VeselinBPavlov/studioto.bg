namespace Studio.Application.Industries.Commands.Create
{
    using FluentValidation;

    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(i => i.Name).MaximumLength(100).NotEmpty();
        }
    }
}
