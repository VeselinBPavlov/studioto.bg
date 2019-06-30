namespace Studio.Application.Services.Commands.Update
{
    using FluentValidation;

    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(i => i.Name).MaximumLength(100).NotEmpty();
        }
    }
}
