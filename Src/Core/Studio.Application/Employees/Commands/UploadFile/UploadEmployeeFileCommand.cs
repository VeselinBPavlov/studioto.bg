namespace Studio.Application.Employees.Commands.UploadFile
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class UploadEmployeeFileCommand : IRequest
    {
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
