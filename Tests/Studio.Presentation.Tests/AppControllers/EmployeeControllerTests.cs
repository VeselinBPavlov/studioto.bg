
// namespace Studio.Presentation.Tests.AppControllers
// {
//     using Microsoft.EntityFrameworkCore;
//     using MyTested.AspNetCore.Mvc;
//     using Studio.Domain.Entities;
//     using Studio.Persistence.Context;
//     using Studio.User.WebApp.Controllers;
//     using Studio.User.WebApp.Models;
//     using Xunit;

//     public class EmployeeControllerTests
//     {
//         [Fact]
//         public void ReturnViewWhenCallingWorkspaceAction()
//             => MyMvc
//                 .Controller<EmployeeController>()
//                 .WithData(db => db
//                     .WithEntities(entities => CreateTestEmployee(context: entities)))
//                 .Calling(c => c.Workspace(1))
//                 .ShouldHave()
//                 .ValidModelState()
//                 .AndAlso()
//                 .ShouldReturn()
//                 .View(With.Default<EmployeeAppointmentDto>());

//         private static Employee CreateTestEmployee(DbContext context)
//         {
//             var employee = new Employee { Id = 1 };

//             context.Add(employee);
//             context.SaveChanges();

//             return employee;
//         }
//     }
// }
