namespace Studio.Application.Appointments.Commands.Create
{
    using System;
    using FluentValidation;

    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(a => a.FirstName).MaximumLength(100).NotEmpty();
            RuleFor(a => a.LastName).MaximumLength(100).NotEmpty();
            RuleFor(cf => cf.Email).MaximumLength(100).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").NotEmpty();
            RuleFor(c => c.Phone).Matches(@"^(\+359|0)(\d{9})$").NotEmpty();
            RuleFor(c => c.ReservationTime).NotEmpty().Must(BeValidDate);
            RuleFor(c => c.ReservationDate).NotEmpty().Must(BeValidDate);
            RuleFor(c => c.TimeBlockHelper).NotEmpty();
        }

        private bool BeValidDate(DateTime reservationTime)
        {
            return reservationTime.Day >= DateTime.UtcNow.Day;
        }
    }
}
