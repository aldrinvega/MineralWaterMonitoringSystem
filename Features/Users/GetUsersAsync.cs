using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Common.Pagination;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Users.Exceptions;

namespace MineralWaterMonitoring.Features.Users;

public class GetUsersAsync
{
    public class UsersAsyncQuery : UserParams, IRequest<PagedList<UsersAsyncQueryResult>>{}

    public class UsersAsyncQueryResult
    {
        public Guid Id { get; set; }

        public string Fullname { get; set; }

        public string UserName { get; set; }
    }

    public class Handler : IRequestHandler<UsersAsyncQuery, PagedList<UsersAsyncQueryResult>>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _dataContext;

            public Handler(IMapper mapper, DataContext dataContext)
            {
                _mapper = mapper;
                _dataContext = dataContext;
            }

            public async Task<PagedList<UsersAsyncQueryResult>> Handle(UsersAsyncQuery request,
                CancellationToken cancellationToken)
            {
                var userQuery = _dataContext.Users
                    .ProjectTo<UsersAsyncQueryResult>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
                if (userQuery == null)
                {
                    throw new NoUsersFoundException();
                }

                var users= await PagedList<UsersAsyncQueryResult>.CreateAsync(userQuery, request.PageNumber, request.PageSize);
                return users;

            }
        }
}