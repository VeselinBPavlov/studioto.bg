namespace Studio.Application.Locations.Commands.UploadFile
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Studio.Application.Interfaces.Core;
    using System.Collections.Generic;

    public class UploadLocationFileCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
