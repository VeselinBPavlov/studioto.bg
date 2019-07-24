namespace Studio.Application.Employees.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.Entities;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateEmployeeCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var location = await this.context.Locations.FindAsync(request.LocationId);

            if (location == null || location.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.Employee, $"{request.FirstName} {request.LastName}", string.Format(GConst.RefereceException, GConst.LocationLower, request.LocationId));
            }

            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LocationId = request.LocationId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Employees.Add(employee);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
