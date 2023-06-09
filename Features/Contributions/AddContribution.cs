using MediatR;
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