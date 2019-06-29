namespace Studio.Application.Cities.Commands.Create
{
    using MediatR;

    public class CreateCityCommand : IRequest
    {
        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
