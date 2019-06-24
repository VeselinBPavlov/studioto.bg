namespace Studio.Application.Industries.Commands.Create
{
    using MediatR;
    using System;

    public class CreateClientCommand : IRequest
    {
        public string Name { get; set; }
    }
}
