namespace Studio.Application.Employees.Queries.GetEmployeeById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LocationId { get; set; }

        public string LocationName{ get; set; }

        public static Expression<Func<Employee, EmployeeViewModel>> Projection
        {
            get
            {
                return employee => new EmployeeViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    LocationId = employee.LocationId,
                    LocationName = employee.Location.Name
                };
            }
        }

        public static EmployeeViewModel Create(Employee employee)
        {
            return Projection.Compile().Invoke(employee);
        }
    }
}