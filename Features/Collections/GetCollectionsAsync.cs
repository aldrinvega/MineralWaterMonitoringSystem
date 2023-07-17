using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Collections.Exceptions;

namespace MineralWaterMonitoring.Features.Collections;

public abstract class GetCollectionsAsync
{
    public class GetCollectionsAsyncQuery : IRequest<IEnumerable<GetCollectionsAsyncQueryResult>> { }

    public class GetCollectionsAsyncQueryResult
    {
        public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public string GroupName { get; set; }
        public int CollectionAmount { get; set; }
        public int CollectedAmount { get; set; }
    }
    
    public class Handler : IRequestHandler<GetCollectionsAsyncQuery, IEnumerable<GetCollectionsAsyncQueryResult>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCollectionsAsyncQueryResult>> Handle(GetCollectionsAsyncQuery request, CancellationToken cancellationToken)
        {
            var collections = await _context.Collections
                .Include(group => group.Groups)
                .ThenInclude(group => group.Payers)
                    .ThenInclude(payer => payer.Contributions)
                .ToListAsync(cancellationToken);

            if (collections == null) throw new NoCollectionFoundExceptions();
            var result = _mapper.Map<IEnumerable<GetCollectionsAsyncQueryResult>>(collections);
            return result;
        }
    }
}