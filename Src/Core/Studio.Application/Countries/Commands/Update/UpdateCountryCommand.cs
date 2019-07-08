namespace Studio.Application.Countries.Commands.Update
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class UpdateCountryCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
