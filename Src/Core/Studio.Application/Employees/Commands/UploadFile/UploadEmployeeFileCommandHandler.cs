namespace Studio.Application.Employees.Commands.UploadFile
{
    using System;
    using System.IO;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Hosting;
    using Studio.Common;

    public class UploadEmployeeFileCommandHandler : IRequestHandler<UploadEmployeeFileCommand>
    {
        private readonly IHostingEnvironment environment;

        public UploadEmployeeFileCommandHandler(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Unit> Handle(UploadEmployeeFileCommand request, CancellationToken cancellationToken)
        {
            var filesPath = $"{environment.WebRootPath}/img/locations/{request.LocationId}/employees/{request.EmployeeId}";

            if (request.Files == null)
            {
                throw new ArgumentException(GConst.PuctureErrorMessage);
            }

            foreach (var file in request.Files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                if (fileName != "\"profile-picture.jpg\"")
                {
                    continue;
                }

                // Ensure the file name is correct
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                var fullFilePath = Path.Combine(filesPath, fileName);

                if (file.Length <= 0)
                {
                    continue;
                }

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Unit.Value;
        }
    }
}
