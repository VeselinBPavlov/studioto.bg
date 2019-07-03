namespace Studio.Application.Employees.Commands.Create
{
    using FluentValidation;

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(c => c.FirstName).MaximumLength(100).NotEmpty();
            RuleFor(c => c.LastName).MaximumLength(100).NotEmpty();
        }
    }
}
