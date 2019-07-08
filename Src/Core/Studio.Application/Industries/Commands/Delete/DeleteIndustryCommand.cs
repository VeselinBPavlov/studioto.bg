namespace Studio.Application.Industries.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteIndustryCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
