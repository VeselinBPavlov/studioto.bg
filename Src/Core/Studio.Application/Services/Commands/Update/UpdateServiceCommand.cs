namespace Studio.Application.Services.Commands.Update
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class UpdateServiceCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IndustryId { get; set; }
    }
}
