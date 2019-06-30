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
        [Fact]
        public async Task ShouldCreateService()
        {
            var industry = new Industry { Name = Common.GConst.IndustryValidName };
            context.Industries.Add(industry);
            this.context.SaveChanges();
            var industryId = context.Industries.SingleOrDefault(x => x.Name == Common.GConst.IndustryValidName).Id;
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateServiceCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateServiceCommand { Name = Common.GConst.ServiceValidName, IndustryId = industryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(Common.GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Services.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForDeletedCountry()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateServiceCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateServiceCommand { Name = "Massage", IndustryId = Common.GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(Common.GConst.ReferenceExceptionMessage, "Creation", nameof(Domain.Entities.Service), "Massage", "industry", Common.GConst.InvalidId), status.Message);
        }
    }
}
