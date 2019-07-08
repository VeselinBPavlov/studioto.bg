namespace Studio.Application.Cities.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteCityCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
