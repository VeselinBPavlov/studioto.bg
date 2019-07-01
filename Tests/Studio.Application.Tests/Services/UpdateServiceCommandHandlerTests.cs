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
            var industry = new Industry { Name = GConst.ValidName };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.ValidName).Id;
            
            var service = new Service { Name = GConst.ValidName, IndustryId = industryId };

            context.Services.Add(service);
            await context.SaveChangesAsync();

            var serviceId = context.Services.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            var sut = new UpdateServiceCommandHandler(context);
            var updatedService = new UpdateServiceCommand { Id = serviceId, Name = "Mars", IndustryId = industryId };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedService, CancellationToken.None));

            var resultId = context.Services.SingleOrDefault(x => x.Name == "Mars").Id;

            Assert.Equal(serviceId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Services.Count());
        }

        [Fact]
        public async void ServiceShouldThrowRefereceException()
        {
            var industryId = GetIndustryId();

            var serviceId = GetServiceId(industryId);

            var sut = new UpdateServiceCommandHandler(context);
            var updatedService = new UpdateServiceCommand { Id = serviceId, Name = GConst.UpdatedName, IndustryId = GConst.InvalidId};

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedService, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Service, serviceId, GConst.IndustryLower,  GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void ServiceShouldThrowNotFoundException()
        {
            var sut = new UpdateServiceCommandHandler(context);
            var updatedService = new UpdateServiceCommand { Id = GConst.InvalidId, Name = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedService, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Service, GConst.InvalidId), status.Message);            
        }
    }
}
