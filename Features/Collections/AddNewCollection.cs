using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Features.Collections;

public class AddNewCollection
{
  public class AddNewCollectionCommand : IRequest<Unit>
  {
    public Guid GroupId { get; set; }
    public int CollectionAmount { get; set; }
  }
  public class Handler : IRequestHandler<AddNewCollectionCommand, Unit>
  {
    private readonly DataContext _context;

    public Handler(DataContext context)
    {
      _context = context;
    }
    public async Task<Unit> Handle(AddNewCollectionCommand command, CancellationToken cancellationToken)
    {
      var collection = new Collection()
      {
        GroupId = command.GroupId,
        CollectionAmount = command.CollectionAmount
      };
      await _context.Collections.AddAsync(collection, cancellationToken);
      await _context.SaveChangesAsync(cancellationToken);
      return Unit.Value;
    }
  }
}