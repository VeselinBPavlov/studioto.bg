namespace Studio.Application.Tests.Services.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Services.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateServiceCommandHandlerTests : CommandTestBase
    {
        private int industryId;
        private Mock<IMediator> mediator;
        private CreateServiceCommandHandler sut;

        public CreateServiceCommandHandlerTests()
        {
            industryId = ArrangeHelper.GetIndustryId(context);
            mediator = new Mock<IMediator>();
            sut = new CreateServiceCommandHandler(context, mediator.Object);
        }

        [Fact]
        public async Task ShouldCreateService()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateServiceCommand { Name = GConst.ValidName, IndustryId = industryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Services.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForDeletedCountry()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateServiceCommand { Name = GConst.ValidName, IndustryId = Common.GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Service, GConst.ValidName, GConst.IndustryLower, GConst.InvalidId), status.Message);
        }
    }
}
