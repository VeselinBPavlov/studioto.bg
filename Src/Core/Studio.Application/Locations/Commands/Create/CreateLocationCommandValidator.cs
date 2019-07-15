namespace Studio.Application.Locations.Commands.Create
{
    using System;
    using FluentValidation;
    using Studio.Domain.Enumerations;

    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(l => l.Name).MaximumLength(100).NotEmpty().WithMessage("Invalid Name");
            RuleFor(l => l.StartDay).NotEmpty().Must(BeValidDayOfWeek).WithMessage("Invalid StartDay"); ;
            RuleFor(l => l.EndDay).NotEmpty().Must(BeValidDayOfWeek).WithMessage("Invalid EndDay"); ;
            RuleFor(l => l.StartHour).NotEmpty().Must(BeValidHour).WithMessage("Invalid StartHour"); ;
            RuleFor(l => l.EndHour).NotEmpty().Must(BeValidHour).WithMessage("Invalid EndHour"); ;
            RuleFor(c => c.Phone).Matches(@"^(\+359|0)(\d{9})$").NotEmpty().WithMessage("Invalid Phone"); ;
            RuleFor(c => c.Slogan).MaximumLength(200).NotEmpty().WithMessage("Invalid Slogan"); ;
            RuleFor(c => c.Description).NotEmpty().WithMessage("Invalid Description"); ;
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
