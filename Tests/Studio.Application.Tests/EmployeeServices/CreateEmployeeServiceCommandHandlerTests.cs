﻿namespace Studio.Application.Tests.EmployeeServices.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.EmployeeServices.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateEmployeeServiceCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateEmployeeService()
        {
            var employeeId = GetEmployeeId(null);
            var serviceId = GetServiceId(null);
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateEmployeeServiceCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateEmployeeServiceCommand { Price = GConst.ValidPrice, EmployeeId = employeeId, ServiceId = serviceId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.EmployeeServices.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidServiceId()
        {
            var employeeId = GetEmployeeId(null);

            var mediator = new Mock<IMediator>();
            var sut = new CreateEmployeeServiceCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateEmployeeServiceCommand { Price = GConst.ValidPrice, EmployeeId = employeeId, ServiceId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.EmployeeService, GConst.InvalidId, GConst.ServiceLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidEmployeeId()
        {
            var serviceId = GetServiceId(null);

            var mediator = new Mock<IMediator>();
            var sut = new CreateEmployeeServiceCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateEmployeeServiceCommand { Price = GConst.ValidPrice, EmployeeId = GConst.InvalidId , ServiceId = serviceId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.EmployeeService, GConst.InvalidId, GConst.EmployeeLower, GConst.InvalidId), status.Message);
        }
    }
}