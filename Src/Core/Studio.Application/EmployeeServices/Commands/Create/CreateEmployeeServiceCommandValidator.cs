namespace Studio.Application.EmployeeServices.Commands.Create
{
    using System;
    using FluentValidation;

    public class CreateEmployeeServiceCommandValidator : AbstractValidator<CreateEmployeeServiceCommand>
    {
        public CreateEmployeeServiceCommandValidator()
        {
            RuleFor(es => es.Price).GreaterThan(0.00M);
            RuleFor(es => es.EmployeeId).NotEqual(0);
            RuleFor(es => es.ServiceId).NotEqual(0);
            RuleFor(es => es.DurationInMinutes).NotEmpty().Must(BeValidDuration);
        }

        private bool BeValidDuration(string duration)
        {
            if (int.TryParse(duration, out int minutes)) 
            {
                if (minutes >= 30 && minutes <= 240) 
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
