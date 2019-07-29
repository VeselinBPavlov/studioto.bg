namespace Studio.Application.Locations.Commands.Update
{
    using System;
    using FluentValidation;
    using Studio.Common;
    using Studio.Domain.Enumerations;

    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        private const string Name = "Име";
        private const string StartDay = "Начален работен ден";
        private const string EndDay = "Краен работен ден";
        private const string StartHour = "Начало на работно време";
        private const string EndHour = "Край на работно време";
        private const string Phone = "Телефон";
        private const string Slogan = "Слоган";
        private const string Description = "Представяне";

        public UpdateLocationCommandValidator()
        {
            RuleFor(l => l.Name)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Name, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Name));
            RuleFor(l => l.StartDay)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, StartDay))
                .Must(this.BeValidDayOfWeek)
                .WithMessage(string.Format(GConst.ErrorInvalidMessage, StartDay));
            RuleFor(l => l.EndDay)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, EndDay))
                .Must(this.BeValidDayOfWeek)
                .WithMessage(string.Format(GConst.ErrorInvalidMessage, EndDay));
            RuleFor(l => l.StartHour)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, StartHour))
                .Must(this.BeValidHour)
                .WithMessage(string.Format(GConst.ErrorInvalidMessage, StartHour));
            RuleFor(l => l.EndHour)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, StartHour))
                .Must(this.BeValidHour)
                .WithMessage(string.Format(GConst.ErrorInvalidMessage, StartHour));
            RuleFor(c => c.Phone)
                .Matches(@"^(\+359|0)(\d{9})$")
                .WithMessage(GConst.ErrorPhoneMessage)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Phone));
            RuleFor(c => c.Slogan)
                .MaximumLength(200)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, Slogan, 1, 200))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Slogan));
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Description));
        }

        private bool BeValidHour(string hour)
        {
            if (int.TryParse(hour, out int workHour)) 
            {
                if (workHour >= 0 && workHour <= 24) 
                {
                    return true;
                }
            }

            return false;
        }

        private bool BeValidDayOfWeek(string day)
        {
            return Enum.TryParse<Workday>(day, true, out Workday result);
        }
    }
}
