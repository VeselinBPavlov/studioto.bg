namespace Studio.Application.EmployeeServices.Commands.Create
{
    using MediatR;

    public class CreateEmployeeServiceCommand : IRequest
    {
        public int EmployeeId { get; set; }

        public int ServiceId { get; set; }

        public decimal Price { get; set; }
    }
}
