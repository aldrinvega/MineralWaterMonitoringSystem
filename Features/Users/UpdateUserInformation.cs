using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Group.Execptions;
using MineralWaterMonitoring.Features.Users.Exceptions;

namespace MineralWaterMonitoring.Features.Users;

public class UpdateUserInformation
{
    public class UpdateUserInformationCommand : IRequest<Unit>
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Fullname
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public Guid GroupId
        {
            get;
            set;
        }

        public class Handler : IRequestHandler<UpdateUserInformationCommand, Unit>
        {
            private DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateUserInformationCommand command, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (true)
                {
                    var groups = await _context.Groups.FirstOrDefaultAsync(x => x.Id == command.GroupId, cancellationToken);
                    if (groups == null)
                        throw new NoGroupsFoundExceptions();
                }
                if (user == null)
                    throw new NoUsersFoundException();
                user.FullName = command.Fullname;
                user.UserName = command.Username;
                user.Password = command.Password;
                user.GroupId = command.GroupId;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;


            }
        }
    }
}