namespace Studio.Application.Industries.Commands.Create
{
    using MediatR;

    public class CreateIndustryCommand : IRequest
    {
        public string Name { get; set; }

        public string Possition { get; set; }
    }
}
