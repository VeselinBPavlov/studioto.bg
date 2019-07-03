namespace Studio.Application.Employees.Commands.Update
{
    using FluentValidation;

    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(c => c.FirstName).MaximumLength(100).NotEmpty();
            RuleFor(c => c.LastName).MaximumLength(100).NotEmpty();
        }
    }
}
