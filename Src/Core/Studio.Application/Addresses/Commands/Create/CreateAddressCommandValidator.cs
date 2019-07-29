namespace Studio.Application.Addresses.Commands.Create
{
    using FluentValidation;
    using Studio.Common;

    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        private const string Street = "Улица";
        private const string Number = "Номер";
        private const string Building = "Блок";
        private const string Entrance = "Вход";
        private const string Floor = "Етаж";
        private const string Apartment = "Апартамент";
        private const string District = "Квартал";
        private const string Longitude = "Дължина";
        private const string Latitude = "Ширина";

        public CreateAddressCommandValidator()
        {
            RuleFor(a => a.Street)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Street, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Street));
            RuleFor(a => a.Number)
                .MaximumLength(10)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Number, 1, 10))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Number));
            RuleFor(a => a.Building)
                .MaximumLength(10)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Building, 1, 10));
            RuleFor(a => a.Entrance)
                .MaximumLength(10)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Entrance, 1, 10));
            RuleFor(a => a.Floor)
                .MaximumLength(10)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Floor, 1, 10));
            RuleFor(a => a.Apartment)
                .MaximumLength(10)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Apartment, 1, 10));
            RuleFor(a => a.District)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, District, 1, 100));
            RuleFor(a => a.Longitude)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorInvalidMessage, Longitude));
            RuleFor(a => a.Latitude)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorInvalidMessage, Latitude));
        }
    }
}
