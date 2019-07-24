namespace Studio.Application.Addresses.Commands.Update
{
    using FluentValidation;

    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(a => a.Street).MaximumLength(100).WithMessage("Invalid Street").NotEmpty().WithMessage("Street is required");
            RuleFor(a => a.Number).MaximumLength(10).WithMessage("Invalid Number").NotEmpty().WithMessage("Number is required");
            RuleFor(a => a.Building).MaximumLength(10).WithMessage("Invalid Building");
            RuleFor(a => a.Entrance).MaximumLength(10).WithMessage("Invalid Entrance");
            RuleFor(a => a.Floor).MaximumLength(10).WithMessage("Invalid Floor");
            RuleFor(a => a.Apartment).MaximumLength(10).WithMessage("Invalid Apartment");
            RuleFor(a => a.District).MaximumLength(100).WithMessage("Invalid District");
        }
    }
}
