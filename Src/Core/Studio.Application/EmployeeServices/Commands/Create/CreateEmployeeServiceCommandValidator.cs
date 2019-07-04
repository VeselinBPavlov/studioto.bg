namespace Studio.Application.EmployeeServices.Commands.Create
{
    using FluentValidation;

    public class CreateEmployeeServiceCommandValidator : AbstractValidator<CreateEmployeeServiceCommand>
    {
        public CreateEmployeeServiceCommandValidator()
        {
            RuleFor(es => es.Price).GreaterThan(0.00M);
            RuleFor(es => es.EmployeeId).NotEqual(0);
            RuleFor(es => es.ServiceId).NotEqual(0);
        }
    }
}
