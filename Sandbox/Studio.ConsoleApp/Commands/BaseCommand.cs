namespace Studio.ConsoleApp.Commands
{
    using MediatR;

    public abstract class BaseCommand
    {
        private IMediator _mediator;

        protected IMediator Mediator;
    }
}
