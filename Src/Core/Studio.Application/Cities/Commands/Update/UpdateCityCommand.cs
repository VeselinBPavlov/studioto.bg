namespace Studio.Application.Cities.Commands.Update
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class UpdateCityCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
