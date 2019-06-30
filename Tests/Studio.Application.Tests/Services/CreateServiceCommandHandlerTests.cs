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
            var industry = new Industry { Name = GlobalConstants.IndustryValidName };
            context.Industries.Add(industry);
            this.context.SaveChanges();
            var industryId = context.Industries.SingleOrDefault(x => x.Name ==  GlobalConstants.IndustryValidName).Id;
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateServiceCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateServiceCommand { Name = GlobalConstants.ServiceValidName, IndustryId = industryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Services.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForDeletedCountry()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateServiceCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateServiceCommand { Name = "Massage", IndustryId = GlobalConstants.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.ServiceCreateFailureExceptionMessageIsNull, "Massage", GlobalConstants.InvalidId), status.Message);
        }
    }
}
