using Studio.Application.Appointments.Commands.Create;
using Studio.Application.Employees.Queries.GetPageEmployeeById;

namespace Studio.User.WebApp.Models
{
    public class EmployeeAppointmentDto
    {
        public EmployeeProfileViewModel EmployeeProfileViewModel { get; set; }

        public CreateAppointmentCommand CreateAppointmentCommand { get; set; } 
    }
}