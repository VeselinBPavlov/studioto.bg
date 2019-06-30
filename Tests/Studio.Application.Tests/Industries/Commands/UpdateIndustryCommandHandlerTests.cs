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
            var industry = new Industry { Name = GlobalConstants.IndustryValidName };

            context.Industries.Add(industry);
            await context.SaveChangesAsync();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GlobalConstants.IndustryValidName).Id;

            var sut = new UpdateIndustryCommandHandler(context);
            var updatedIndustry = new UpdateIndustryCommand { Id = industryId, Name = GlobalConstants.IndustrySecondValidName };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedIndustry, CancellationToken.None));

            var resultId = context.Industries.SingleOrDefault(x => x.Name == GlobalConstants.IndustrySecondValidName).Id;

            Assert.Equal(industryId, resultId);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Industries.Count());
        }

        [Fact]
        public async void IndustryShouldThrowNotFoundException()
        {
            var sut = new UpdateIndustryCommandHandler(context);
            var updatedIndustry = new UpdateIndustryCommand { Id = GlobalConstants.InvalidId, Name = GlobalConstants.IndustrySecondValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedIndustry, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.NotFoundExceptionMessage, nameof(Industry), GlobalConstants.InvalidId), status.Message);            
        }
    }
}
