namespace Studio.Application.Addresses.Commands.Create
{
    using FluentValidation;

    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(a => a.Street).MaximumLength(100).NotEmpty();
            RuleFor(a => a.Number).MaximumLength(10).NotEmpty();
            RuleFor(a => a.Building).MaximumLength(10);
            RuleFor(a => a.Entrance).MaximumLength(10);
            RuleFor(a => a.Floor).MaximumLength(10);
            RuleFor(a => a.Apartment).MaximumLength(10);
            RuleFor(a => a.District).MaximumLength(100);
        }
    }
}
