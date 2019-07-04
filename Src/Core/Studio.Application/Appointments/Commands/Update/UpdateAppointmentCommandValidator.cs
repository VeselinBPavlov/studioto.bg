namespace Studio.Application.Appointments.Commands.Update
{
    using FluentValidation;
    using System;

    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(a => a.FirstName).MaximumLength(100).NotEmpty();
            RuleFor(a => a.LastName).MaximumLength(100).NotEmpty();
            RuleFor(cf => cf.Email).MaximumLength(100).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").NotEmpty();
            RuleFor(c => c.Phone).Matches(@"^(\+359|0)(\d{9})$").NotEmpty();
            RuleFor(c => c.ReservetionTime).NotEmpty().Must(BeValidDate);
        }

        private bool BeValidDate(DateTime reservationTime)
        {
            return reservationTime.Day >= DateTime.UtcNow.Day;
        }
    }
}
