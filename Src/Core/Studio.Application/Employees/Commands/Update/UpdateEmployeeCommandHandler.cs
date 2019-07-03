namespace Studio.Application.Employees.Commands.Update
{
    using Studio.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Exceptions;
    using Interfaces.Persistence;
    using System;
    using System.Linq;
    using Studio.Common;

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateEmployeeCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await this.context.Employees
                .SingleOrDefaultAsync(c => c.Id == request.Id && c.IsDeleted != true, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException(GConst.Employee, request.Id);
            }

            var location = await this.context.Locations.FindAsync(request.LocationId);

            if (location == null || location.IsDeleted == true)
            {
                throw new UpdateFailureException(GConst.Employee, request.Id, string.Format(GConst.RefereceException, GConst.LocationLower, request.LocationId));
            }

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.LocationId = request.LocationId;
            employee.ModifiedOn = DateTime.UtcNow;

            this.context.Employees.Update(employee);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
