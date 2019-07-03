namespace Studio.Application.Appointments.Commands.Create
{
    using System;
    using System.Globalization;
    using FluentValidation;

    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(a => a.FirstName).MaximumLength(100).NotEmpty();
            RuleFor(a => a.LastName).MaximumLength(100).NotEmpty();
            RuleFor(cf => cf.Email).MaximumLength(100).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").NotEmpty();
            RuleFor(c => c.Phone).Matches(@"^(\+359|0)(\d{9})$").NotEmpty();
            RuleFor(c => c.ReservetionTime).NotEmpty().Must(BeValidDate);
            RuleFor(c => c.UserId).NotEmpty();
        }

        private bool BeValidDate(string reservationTime)
        {
            DateTime reservation;
            return DateTime.TryParseExact(reservationTime, "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out reservation);
        }
    }
}
