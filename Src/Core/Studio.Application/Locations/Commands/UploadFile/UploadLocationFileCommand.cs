namespace Studio.Application.Locations.Commands.UploadFile
{
    using System.Collections.Generic;
    using Interfaces.Core;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class UploadLocationFileCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
