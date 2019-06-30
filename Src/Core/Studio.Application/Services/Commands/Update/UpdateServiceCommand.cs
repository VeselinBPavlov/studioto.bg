namespace Studio.Application.Services.Commands.Update
{
    using MediatR;

    public class UpdateServiceCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IndustryId { get; set; }
    }
}
