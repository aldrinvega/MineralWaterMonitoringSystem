using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;

namespace MineralWaterMonitoring.Features.Contributions;

public class GetContributionsAsync
{
    public class GetContributionsAsyncQuery : IRequest<IEnumerable<GetContributionsAsyncResult>> { }

    public class GetContributionsAsyncResult
    {
        public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public Guid PayerId { get; set; }
        public string PayerName { get; set; }
        public int ContributionAmount { get; set; }
    }

    public class Handler : IRequestHandler<GetContributionsAsyncQuery, IEnumerable<GetContributionsAsyncResult>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetContributionsAsyncResult>> Handle(GetContributionsAsyncQuery request, CancellationToken cancellationToken)
        {
            var contributions = await _context.Contributions
                .Include(payers => payers.Payer)
                .ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<GetContributionsAsyncResult>>(contributions);
            return  result;
        }
    }
}