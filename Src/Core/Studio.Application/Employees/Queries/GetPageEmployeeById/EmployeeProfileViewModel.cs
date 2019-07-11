namespace Studio.Application.Employees.Queries.GetPageEmployeeById
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class EmployeeProfileViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Possitions { get; set; }

        public string LocationAddress { get; set; }

        public string LocationPhone { get; set; }

        public string LocationSlogan { get; set; }

        public string LocationDescription { get; set; }

        public string LocationStartDay { get; set; }

        public string LocationEndDay { get; set; }

        public string LocationStartHour { get; set; }

        public string LocationEndHour { get; set; }

        public static Expression<Func<Employee, EmployeeProfileViewModel>> Projection
        {
            get
            {
                return employee => new EmployeeProfileViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Possitions = string.Join(", ", employee.EmployeeServices.Select(es => es.Service.Industry.Possition)),
                    LocationAddress = employee.Location.Address.AddressFormat.ToString(),
                    LocationPhone = employee.Location.Phone,
                    LocationSlogan = employee.Location.Slogan,
                    LocationDescription = employee.Location.Description,
                    LocationStartDay = employee.Location.StartDay.ToString(),
                    LocationEndDay = employee.Location.EndDay.ToString(),
                    LocationStartHour = employee.Location.StartHour,
                    LocationEndHour = employee.Location.EndHour
                };
            }
        }

        public static EmployeeProfileViewModel Create(Employee employee)
        {
            return Projection.Compile().Invoke(employee);
        }
    }
}