namespace Studio.Application.Addresses.Commands.Update
{
    using FluentValidation;

    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(i => i.Street).MaximumLength(100).NotEmpty();
            RuleFor(i => i.Number).MaximumLength(10).NotEmpty();
            RuleFor(i => i.Building).MaximumLength(10);
            RuleFor(i => i.Entrance).MaximumLength(10);
            RuleFor(i => i.Floor).MaximumLength(10);
            RuleFor(i => i.Apartment).MaximumLength(10);
            RuleFor(i => i.District).MaximumLength(100);
        }
    }
}
