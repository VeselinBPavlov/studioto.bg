namespace Studio.Application.Addresses.Commands.Update
{
    using FluentValidation;

    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(а => а.Street).MaximumLength(100).NotEmpty();
            RuleFor(а => а.Number).MaximumLength(10).NotEmpty();
            RuleFor(а => а.Building).MaximumLength(10);
            RuleFor(а => а.Entrance).MaximumLength(10);
            RuleFor(а => а.Floor).MaximumLength(10);
            RuleFor(а => а.Apartment).MaximumLength(10);
            RuleFor(а => а.District).MaximumLength(100);
        }
    }
}
