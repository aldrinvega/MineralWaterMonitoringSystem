using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Payers.Exceptions;

namespace MineralWaterMonitoring.Features.Payers;

public class GetPayersAsync
{
    public class GetPayersAsyncQuery : IRequest<IEnumerable<GetPayersAsyncQueryResult>>{}

    public class GetPayersAsyncQueryResult
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int Balance { get; set; }
    }
    public class Handler : IRequestHandler<GetPayersAsyncQuery, IEnumerable<GetPayersAsyncQueryResult>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<GetPayersAsyncQueryResult>> Handle(GetPayersAsyncQuery request, CancellationToken cancellationToken)
        {
            var payers = await _context.Payers
                .Include(groups => groups.Groups)
                .ToListAsync(cancellationToken: cancellationToken);
            if (payers == null)
                throw new NoPayersFoundException();

            var result = _mapper.Map<IEnumerable<GetPayersAsyncQueryResult>>(payers);
            return result;
        }
    }
}