using System;
using System.Linq;
using System.Linq.Expressions;
using Studio.Domain.Entities;

namespace Studio.Application.Locations.Queries.GetLocationByIdPage
{
    public class EmployeeLocationPageViewModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Possitions { get; set; }

        public static Expression<Func<Employee, EmployeeLocationPageViewModel>> Projection
        {
            get
            {
                return employee => new EmployeeLocationPageViewModel
                {
                    EmployeeId = employee.Id,
                    EmployeeName = $"{employee.FirstName} {employee.LastName}",
                    Possitions = string.Join(", ", employee.EmployeeServices.Select(z => z.Service.Industry.Possition).Distinct())                    
                };
            }
        }

        public static EmployeeLocationPageViewModel Create(Employee employee)
        {
            return Projection.Compile().Invoke(employee);
        }
    }
}