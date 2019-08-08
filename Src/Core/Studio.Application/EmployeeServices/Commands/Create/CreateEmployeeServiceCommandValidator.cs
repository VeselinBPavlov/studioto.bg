namespace Studio.Application.EmployeeServices.Commands.Create
{
    using System;
    using FluentValidation;
    using Studio.Common;

    public class CreateEmployeeServiceCommandValidator : AbstractValidator<CreateEmployeeServiceCommand>
    {
        private const string Duration = "Продължителност на услугата";

        public CreateEmployeeServiceCommandValidator()
        {
            RuleFor(es => es.Price)
                .GreaterThan(0.00M)
                .WithMessage(GConst.ErrorPriceMessage)
                .NotEmpty()
                .WithMessage(GConst.ErrorInvalidMessage);
            RuleFor(es => es.EmployeeId).NotEqual(0);
            RuleFor(es => es.ServiceId).NotEqual(0);
            RuleFor(es => es.DurationInMinutes)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, Duration))                
                .Must(this.BeValidDuration)
                .WithMessage(GConst.ErrorInvalidMessage);                
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
