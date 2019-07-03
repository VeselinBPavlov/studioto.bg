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
            var industryId = GetIndustryId();

            var serviceId = GetServiceId(industryId);

            var sut = new UpdateServiceCommandHandler(context);
            var updatedService = new UpdateServiceCommand { Id = serviceId, Name = GConst.ValidName, IndustryId = industryId };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedService, CancellationToken.None));

            var resultId = context.Services.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            Assert.Equal(serviceId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Services.Count());
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
