using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Users.Exceptions;

namespace MineralWaterMonitoring.Features.Users;

public class UpdateUserStatus
{
    public class UpdateUserStatusCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserStatusCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (user == null)
            {
                throw new NoUsersFoundException();
            }

            user.Status = command.Status;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}