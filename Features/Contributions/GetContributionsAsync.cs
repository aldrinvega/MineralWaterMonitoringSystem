using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Common.Pagination;
using MineralWaterMonitoring.Data;

namespace MineralWaterMonitoring.Features.Contributions;

public class GetContributionsAsync
{
    public class GetContributionsAsyncQuery : UserParams, IRequest<PagedList<GetContributionsAsyncResult>> { }

    public class GetContributionsAsyncResult
    {
        public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public Guid PayerId { get; set; }
        public string PayerName { get; set; }
        public int ContributionAmount { get; set; }
    }

    public class Handler : IRequestHandler<GetContributionsAsyncQuery, PagedList<GetContributionsAsyncResult>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Pagination is applied here
        public async Task<PagedList<GetContributionsAsyncResult>> Handle(GetContributionsAsyncQuery request, CancellationToken cancellationToken)
        {
            var contributions = _context.Contributions
                .Include(payers => payers.Payer)
                .ProjectTo<GetContributionsAsyncResult>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            // Create a paged list based on the page number and page size from the request
            var result = await PagedList<GetContributionsAsyncResult>
                .CreateAsync(
                    contributions,
                    request.PageNumber,
                    request.PageSize
                );
            return result;
        }
    }
}