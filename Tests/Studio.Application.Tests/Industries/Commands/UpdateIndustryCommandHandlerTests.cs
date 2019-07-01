namespace Studio.Application.Tests.Industries.Commands
{
    using MediatR;
    using Studio.Application.Industries.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateIndustryCommandHandlerTests : CommandTestBase
    {   
        [Fact]
        public async void IndustryShouldUpdateCorrect()
        {
            var industryId = GetIndustryId();

            var sut = new UpdateIndustryCommandHandler(context);
            var updatedIndustry = new UpdateIndustryCommand { Id = industryId, Name = GConst.ValidName };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedIndustry, CancellationToken.None));

            var resultId = context.Industries.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            Assert.Equal(industryId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Industries.Count());
        }

        [Fact]
        public async void IndustryShouldThrowNotFoundException()
        {
            var sut = new UpdateIndustryCommandHandler(context);
            var updatedIndustry = new UpdateIndustryCommand { Id = GConst.InvalidId, Name = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedIndustry, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Industry, GConst.InvalidId), status.Message);            
        }
    }
}
