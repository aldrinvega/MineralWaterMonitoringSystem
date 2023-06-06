using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Domain;
using MineralWaterMonitoring.Features.Group.Execptions;

namespace MineralWaterMonitoring.Features.Group;

public class AddNewGroup
{
    public class AddNewGroupCommand : IRequest<Unit>
    {
        public string GroupCode { get;
            set;
        }

        public string GroupName { get;
            set;
        }
    }

    public class Handler : IRequestHandler<AddNewGroupCommand, Unit>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Unit> Handle(AddNewGroupCommand command, CancellationToken cancellationToken)
        {
            var groupExist = await _dataContext.Groups.FirstOrDefaultAsync(x => x.GroupName == command.GroupName);

            if (groupExist != null)
            {
                throw new GroupAlreadyExists(command.GroupName);
            }

            var groups = new Groups()
            {
                GroupName = command.GroupName,
                GroupCode = command.GroupCode
            };
           await _dataContext.Groups.AddAsync(groups, cancellationToken);
           await _dataContext.SaveChangesAsync(cancellationToken);
           return Unit.Value;
        }
    }
}