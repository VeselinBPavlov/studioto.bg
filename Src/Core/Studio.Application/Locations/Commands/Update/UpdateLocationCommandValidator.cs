﻿namespace Studio.Application.Locations.Commands.Update
{
    using System;
    using FluentValidation;
    using Studio.Domain.Enumerations;

    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(l => l.Name).MaximumLength(100).NotEmpty();
            RuleFor(l => l.StartDay).NotEmpty().Must(this.BeValidDayOfWeek);
            RuleFor(l => l.EndDay).NotEmpty().Must(this.BeValidDayOfWeek);
            RuleFor(l => l.StartHour).NotEmpty().Must(this.BeValidHour);
            RuleFor(l => l.EndHour).NotEmpty().Must(this.BeValidHour);
            RuleFor(c => c.Phone).Matches(@"^(\+359|0)(\d{9})$").NotEmpty();
            RuleFor(c => c.Slogan).MaximumLength(200).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
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
