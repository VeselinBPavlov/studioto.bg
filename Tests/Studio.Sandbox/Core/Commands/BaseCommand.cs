namespace Studio.Sandbox.Core.Commands
{
    using MediatR;

    public abstract class BaseCommand
    {
        private readonly IMediator mediator;

        public BaseCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
