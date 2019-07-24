using System;
using System.Linq.Expressions;
using System.Linq;

using AutoMapper;
using Studio.Application.Interfaces.Mapping;
using Studio.Domain.Entities;

namespace Studio.Application.Employees.Queries.GetPageEmployeeById
{
    public class ServiceProfileViewModel
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string Duration { get; set; }

        public decimal Price { get; set; }

        public static Expression<Func<EmployeeService, ServiceProfileViewModel>> Projection
        {
            get
            {                
                return employeeService => new ServiceProfileViewModel
                {
                    ServiceId = employeeService.ServiceId,
                    ServiceName = employeeService.Service.Name,
                    Duration = employeeService.DurationInMinutes,
                    Price = employeeService.Price                    
                };
            }
        }

        public static ServiceProfileViewModel Create(EmployeeService employeeService)
        {
            return Projection.Compile().Invoke(employeeService);
        }
    }
}