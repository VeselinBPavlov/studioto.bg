namespace Studio.Application.Locations.Commands.UploadFile
{
    using System;
    using System.IO;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Hosting;
    using Studio.Common;

    public class UploadLocationFileCommandHandler : IRequestHandler<UploadLocationFileCommand>
    {
        private readonly IHostingEnvironment environment;

        public UploadLocationFileCommandHandler(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Unit> Handle(UploadLocationFileCommand request, CancellationToken cancellationToken)
        {
            var filesPath = $"{this.environment.WebRootPath}/img/locations/{request.Id}";

            if (request.Files == null)
            {
                throw new ArgumentException(GConst.PuctureErrorMessage);
            }

            foreach (var file in request.Files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                if (fileName != "\"logo.jpg\"" 
                    && fileName != "\"1.jpg\"" 
                    && fileName != "\"2.jpg\""
                    && fileName != "\"3.jpg\"" 
                    && fileName != "\"4.jpg\"")
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
