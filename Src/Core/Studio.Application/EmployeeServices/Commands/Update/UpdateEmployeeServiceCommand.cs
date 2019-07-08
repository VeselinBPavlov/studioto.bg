namespace Studio.Application.EmployeeServices.Commands.Update
{
    using MediatR;

    public class UpdateEmployeeServiceCommand : IRequest
    {
        public int EmployeeId { get; set; }

        public int ServiceId { get; set; }

        public decimal Price { get; set; }

        public string DurationInMinutes { get; set; }
    }
}
