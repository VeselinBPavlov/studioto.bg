namespace Studio.Application.Services.Commands.Update
{
    using FluentValidation;

    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(s => s.Name).MaximumLength(100).NotEmpty();
        }
    }
}
