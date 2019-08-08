namespace Studio.User.WebApp.Models
{
    using Application.Appointments.Commands.Create;
    using Application.Employees.Queries.GetPageEmployeeById;

    public class EmployeeAppointmentDto
    {
        public EmployeeProfileViewModel EmployeeProfileViewModel { get; set; }

        public CreateAppointmentCommand CreateAppointmentCommand { get; set; } 
    }
}