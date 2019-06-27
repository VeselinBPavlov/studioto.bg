namespace Studio.Application.Countries.Commands.Update
{
    using MediatR;

    public class UpdateCountryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Possition { get; set; }
    }
}
