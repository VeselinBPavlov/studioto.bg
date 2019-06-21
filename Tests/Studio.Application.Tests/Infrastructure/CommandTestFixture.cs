namespace Studio.Application.Tests.Infrastructure
{
    using MediatR;
    using Moq;
    using Xunit;

    public class CommandTestFixture : BaseTestFixture
    {
        public IMediator Mediator { get; private set; }
        public Mock Mock { get; set; }

        public CommandTestFixture()
        {
            this.Mock = new Mock<IMediator>();
            this.Mediator = (IMediator)Mock.Object;
        }
        
        [CollectionDefinition("CreateEntities")]
        public class CreateEntity : ICollectionFixture<CommandTestFixture> { }
    }
}
