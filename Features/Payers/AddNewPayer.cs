using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Payers.Exceptions;

namespace MineralWaterMonitoring.Features.Payers;

public class AddNewPayer
{
    public class AddNewPayerCommand : IRequest<Unit>
    {
        public string FullName { get; set; }
        public Guid GroupId { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewPayerCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(AddNewPayerCommand command, CancellationToken cancellationToken)
        {
            var payerVerify = await _context.Payers.FirstOrDefaultAsync(x => x.Fullname == command.FullName, cancellationToken: cancellationToken);
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == command.GroupId, cancellationToken: cancellationToken);
            if (payerVerify != null)
            {
                throw new PayerIsAlreadyExistException(command.FullName);
            }

            if (group == null)
            {
                throw new GroupIsNotExistException();
            }

            var payers = new Domain.Payers
            {
                Fullname = command.FullName,
                GroupId = command.GroupId
            };

            await _context.Payers.AddAsync(payers, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}