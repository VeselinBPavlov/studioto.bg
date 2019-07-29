namespace Studio.Application.EmployeeServices.Commands.Update
{
    using FluentValidation;
    using Studio.Common;

    public class UpdateEmployeeServiceCommandValidator : AbstractValidator<UpdateEmployeeServiceCommand>
    {
        private const string Duration = "Продължителност на услугата";
        public UpdateEmployeeServiceCommandValidator()
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
