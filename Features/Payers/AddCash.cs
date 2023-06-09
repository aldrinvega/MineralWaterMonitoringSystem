using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;

namespace MineralWaterMonitoring.Features.Payers.Exceptions;

public class AddCash
{
    public class AddCashCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }

    public class Handler : IRequestHandler<AddCashCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddCashCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Payers.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (user == null)
                throw new NoPayersFoundException();

            user.Balance += command.Amount;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}