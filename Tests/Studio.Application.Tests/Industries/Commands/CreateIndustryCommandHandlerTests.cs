using Studio.Application.Industries.Commands.Create;
using Studio.Application.Tests.Infrastructure;
using Studio.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;
using Xunit;
using MediatR;
using Moq;

namespace Studio.Application.Tests.Industries.Commands 
{
    [CollectionDefinition("CreateEntities")]
    public class CreateIndustryCommandHandlerTests : IClassFixture<CommandTestFixture>
    {
        private readonly CommandTestFixture fixture;

        public CreateIndustryCommandHandlerTests(CommandTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Create()
        {
            var sut = new CreateIndustryCommandHandler(this.fixture.Context, this.fixture.Mediator);

            var result = await sut.Handle(new CreateIndustryCommand { Name = "Hairstyle" }, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
