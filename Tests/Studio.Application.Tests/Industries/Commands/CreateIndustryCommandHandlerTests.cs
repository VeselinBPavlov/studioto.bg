﻿namespace Studio.Application.Tests.Industries.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    [CollectionDefinition("CommandCollection")]
    public class CreateIndustryCommandHandlerTests : BaseCommandTests
    {
        public CreateIndustryCommandHandlerTests(CommandTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldCreateIndustry()
        {
            var sut = new CreateIndustryCommandHandler(Fixture.Context, Fixture.Mediator);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateIndustryCommand { Name = GlobalConstants.IndustryValidName }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, this.Fixture.Context.Industries.Count());
        }
    }
}
