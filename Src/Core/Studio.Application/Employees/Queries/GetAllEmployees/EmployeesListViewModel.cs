namespace Studio.Application.Employees.Queries.GetAllEmployees
{
    using System.Collections.Generic;

    public class EmployeesListViewModel
    {
        public IList<EmployeeAllViewModel> Employees { get; set; } 
    }
}