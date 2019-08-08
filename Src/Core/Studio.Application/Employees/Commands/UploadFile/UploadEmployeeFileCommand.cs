namespace Studio.Application.Employees.Commands.UploadFile
{
    using System.Collections.Generic;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class UploadEmployeeFileCommand : IRequest
    {
        public int EmployeeId { get; set; }

        public int LocationId { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
