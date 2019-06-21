using MediatR;
using System.Threading.Tasks;

namespace Studio.Sandbox.Core.Commands
{
    public class CreateIndustryCommand : BaseCommand
    {
        private readonly IMediator mediator;

        public CreateIndustryCommand(IMediator mediator) 
            : base(mediator)
        {

        }

        //public async string Execute(string[] data)
        //{
        //    string name = data[0];
        //    var command = new CreateIndustryCommand(mediator);
        //    string result = await command.mediator.Send(name);

        //    return "";
        //}
    }
}
