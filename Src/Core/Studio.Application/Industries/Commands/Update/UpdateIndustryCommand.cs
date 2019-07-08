namespace Studio.Application.Industries.Commands.Update
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class UpdateIndustryCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Possition { get; set; }
    }
}
