namespace Studio.Application.Tests.Services.Commands
{
    using MediatR;
    using Studio.Application.Services.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateServiceCommandHandlerTests : CommandTestBase
    {   
        [Fact]
        public async void ServiceShouldUpdateCorrect()
        {
            var industry = new Industry { Name = GlobalConstants.IndustryValidName };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GlobalConstants.IndustryValidName).Id;
            
            var service = new Service { Name = GlobalConstants.ServiceValidName, IndustryId = industryId };

            context.Services.Add(service);
            await context.SaveChangesAsync();

            var serviceId = context.Services.SingleOrDefault(x => x.Name == GlobalConstants.ServiceValidName).Id;

            var sut = new UpdateServiceCommandHandler(context);
            var updatedService = new UpdateServiceCommand { Id = serviceId, Name = "Mars", IndustryId = industryId };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedService, CancellationToken.None));

            var resultId = context.Services.SingleOrDefault(x => x.Name == "Mars").Id;

            Assert.Equal(serviceId, resultId);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Services.Count());
        }

        [Fact]
        public async void ServiceShouldThrowNotFoundException()
        {
            var sut = new UpdateServiceCommandHandler(context);
            var updatedService = new UpdateServiceCommand { Id = GlobalConstants.InvalidId, Name = GlobalConstants.ServiceValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedService, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.NotFoundExceptionMessage, nameof(Service), GlobalConstants.InvalidId), status.Message);            
        }
    }
}
