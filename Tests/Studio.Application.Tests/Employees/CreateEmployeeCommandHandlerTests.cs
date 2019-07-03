﻿namespace Studio.Application.Tests.Employees.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Employees.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateEmployeeCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateEmployee()
        {
            var locationId = GetLocationId(null, null);
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateEmployeeCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateEmployeeCommand { FirstName = GConst.ValidName, LastName = GConst.ValidName, LocationId = locationId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Employees.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForDeletedLocation()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateEmployeeCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateEmployeeCommand { FirstName = GConst.ValidName, LastName = GConst.ValidName, LocationId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Employee, $"{GConst.ValidName} {GConst.ValidName}", GConst.LocationLower, GConst.InvalidId), status.Message);
        }
    }
}
