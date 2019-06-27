namespace Studio.Application.Countries.Commands.Create
{
    using MediatR;

    public class CreateCountryCommand : IRequest
    {
        public string Name { get; set; }
    }
}
