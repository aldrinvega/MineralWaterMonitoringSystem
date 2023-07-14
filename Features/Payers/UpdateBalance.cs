using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;

namespace MineralWaterMonitoring.Features.Payers;

public abstract class UpdateBalance
{
    public class UpdateBalanceCommand : IRequest<Unit>
    {
        public Guid PayerId { get; set; }
        public Guid ContributionId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateBalanceCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBalanceCommand command, CancellationToken cancellationToken)
        {
            var contribution = await _context.Contributions.FirstOrDefaultAsync(x => x.Id == command.ContributionId, cancellationToken);
            var user = await _context.Payers.FirstOrDefaultAsync(x => x.Id == command.PayerId, cancellationToken);
            var remainingBalance = user.Balance - contribution.ContributionAmount;
            user.Balance = remainingBalance;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}