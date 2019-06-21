namespace Studio.Application.Tests.Infrastructure
{
    using Studio.Application.Tests.Infrastructure;
    using Xunit;

    [CollectionDefinition("CommandCollection")]
    public class BaseCommandTests : IClassFixture<CommandTestFixture>
    {
        private readonly CommandTestFixture fixture;
        protected CommandTestFixture Fixture => fixture;

        protected BaseCommandTests(CommandTestFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
