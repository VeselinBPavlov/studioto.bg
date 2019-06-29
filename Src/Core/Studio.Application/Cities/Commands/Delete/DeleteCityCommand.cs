namespace Studio.Application.Cities.Commands.Delete
{
    using MediatR;

    public class DeleteCityCommand : IRequest
    {
        public int Id { get; set; }
    }
}
