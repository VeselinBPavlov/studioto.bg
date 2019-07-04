namespace Studio.Application.Tests.LocationIndustries.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.LocationIndustries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateLocationIndustryCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateLocationIndustry()
        {
            var locationId = GetLocationId(null, null);
            var industryId = GetIndustryId();
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateLocationIndustryCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateLocationIndustryCommand { Description = GConst.ValidName, LocationId = locationId, IndustryId = industryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.LocationIndustries.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidIndustryId()
        {
            var locationId = GetLocationId(null, null);

            var mediator = new Mock<IMediator>();
            var sut = new CreateLocationIndustryCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateLocationIndustryCommand { Description = GConst.ValidName, LocationId = locationId, IndustryId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.LocationIndustry, GConst.InvalidId, GConst.IndustryLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidLocationId()
        {
            var industryId = GetIndustryId();

            var mediator = new Mock<IMediator>();
            var sut = new CreateLocationIndustryCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateLocationIndustryCommand { Description = GConst.ValidName, LocationId = GConst.InvalidId , IndustryId = industryId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.LocationIndustry, GConst.InvalidId, GConst.LocationLower, GConst.InvalidId), status.Message);
        }
    }
}
