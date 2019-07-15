namespace Studio.Application.Cities.Queries.GetEmployeeServiceById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class EmployeeServiceViewModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        public string DurationInMinutes { get; set; }

        public static Expression<Func<EmployeeService, EmployeeServiceViewModel>> Projection
        {
            get
            {
                return employeeService => new EmployeeServiceViewModel
                {
                    EmployeeId = employeeService.EmployeeId,
                    EmployeeName = employeeService.Employee.FirstName + " " + employeeService.Employee.LastName,                    
                    ServiceId = employeeService.ServiceId,
                    ServiceName = employeeService.Service.Name,
                    Price = employeeService.Price,
                    DurationInMinutes = employeeService.DurationInMinutes
                };
            }
        }

        public static EmployeeServiceViewModel Create(EmployeeService employeeService)
        {
            return Projection.Compile().Invoke(employeeService);
        }
    }
}