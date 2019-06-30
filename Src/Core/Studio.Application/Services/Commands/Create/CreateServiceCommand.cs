namespace Studio.Application.Services.Commands.Create
{
    using MediatR;

    public class CreateServiceCommand : IRequest
    {
        public string Name { get; set; }

        public int IndustryId { get; set; }
    }
}
