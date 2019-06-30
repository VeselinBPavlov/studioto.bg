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
            var industry = new Industry { Name = GConst.IndustryValidName };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.IndustryValidName).Id;

            var sut = new UpdateIndustryCommandHandler(context);
            var updatedIndustry = new UpdateIndustryCommand { Id = industryId, Name = GConst.IndustrySecondValidName };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedIndustry, CancellationToken.None));

            var resultId = context.Industries.SingleOrDefault(x => x.Name == GConst.IndustrySecondValidName).Id;

            Assert.Equal(industryId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Industries.Count());
        }

        [Fact]
        public async void IndustryShouldThrowNotFoundException()
        {
            var sut = new UpdateIndustryCommandHandler(context);
            var updatedIndustry = new UpdateIndustryCommand { Id = GConst.InvalidId, Name = GConst.IndustrySecondValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedIndustry, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, nameof(Industry), GConst.InvalidId), status.Message);            
        }
    }
}
