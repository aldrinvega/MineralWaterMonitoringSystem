using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Domain;
using MineralWaterMonitoring.Features.Users.Exceptions;

namespace MineralWaterMonitoring.Features.Users;

public class GetUsersAsync
{
    public class UsersAsyncQuery : IRequest<IEnumerable<UsersAsyncQueryResult>>{}

    public class UsersAsyncQueryResult
    {
        public Guid Id { get; set; }

        public string Fullname { get; set; }

        public string UserName { get; set; }
    }

    public class Handler : IRequestHandler<UsersAsyncQuery, IEnumerable<UsersAsyncQueryResult>>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _dataContext;

            public Handler(IMapper mapper, DataContext dataContext)
            {
                _mapper = mapper;
                _dataContext = dataContext;
            }

            public async Task<IEnumerable<UsersAsyncQueryResult>> Handle(UsersAsyncQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users
                    .ToListAsync(cancellationToken);
                if (user == null)
                {
                    throw new NoUsersFoundException();
                }

                var result = _mapper.Map<IEnumerable<UsersAsyncQueryResult>>(user);
                return result;

            }
        }
}