namespace Studio.Application.Cities.Commands.Update
{
    using MediatR;

    public class UpdateCityCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
