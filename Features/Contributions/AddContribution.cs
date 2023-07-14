using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;

namespace MineralWaterMonitoring.Features.Contributions;

public class AddContribution
{
    public class AddContributionCommand : IRequest<Unit>
    {
        public Guid CollectionId { get; set; }
        public Guid PayerId { get; set; }
        public int ContributionAmount { get; set; }
    }
    public class Handler : IRequestHandler<AddContributionCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddContributionCommand command, CancellationToken cancellationToken)
        {

            var collection = await _context.Collections.FirstOrDefaultAsync(x => x.Id == command.CollectionId, cancellationToken);
            var contributionSum = _context.Contributions.Where(x => x.CollectionId == command.CollectionId)
                .Sum(x => x.ContributionAmount);
            if (collection.CollectionAmount == contributionSum)
            {
                throw new Exception("Amount is already collected");
            }
            
            var contributions = new Domain.Contributions()
            {
                CollectionId = command.CollectionId,
                PayerId = command.PayerId,
                ContributionAmount = command.ContributionAmount
            };
            await _context.Contributions.AddAsync(contributions, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}