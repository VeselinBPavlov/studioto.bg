namespace Studio.Application.Industries.Commands.Update
{
    using MediatR;

    public class UpdateIndustryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Possition { get; set; }
    }
}
