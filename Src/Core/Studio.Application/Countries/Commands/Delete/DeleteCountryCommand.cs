namespace Studio.Application.Countries.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteCountryCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
