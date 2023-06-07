using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Domain;
using MineralWaterMonitoring.Features.Role.Exceptions;

namespace MineralWaterMonitoring.Features.Role;

public class AddNewRole
{
    public class AddNewRoleCommand : IRequest<Unit>
    {
        public string RoleName { get; set; }
    }

    public class Handler : IRequestHandler<AddNewRoleCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewRoleCommand command, CancellationToken cancellationToken)
        {
            var roleExist = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == command.RoleName, cancellationToken);

            if (roleExist != null)
                throw new RoleAlreadyExistException(command.RoleName);

            var role = new Roles()
            {
                RoleName = command.RoleName
            };
            await _context.Roles.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}