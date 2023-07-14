using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Common.Pagination;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Group.Execptions;
using MineralWaterMonitoring.Features.Users;

namespace MineralWaterMonitoring.Features.Group;

public class GetGroupsAsync
{
    public class GroupsAsyncQuery : UserParams, IRequest<GroupsAsyncQueryResult> {}
	
	
    public class DTOs
    {
        public class Group
        {
            public Guid Id
            {
                get;
                set;
            }

            public string GroupCode
            {
                get;
                set;
            }

            public string GroupName
            {
                get;
                set;
            }
			
            public ICollection<Payers> Payers
            {
                get;
                set;
            }
        }
		
        public class Payers
        {
            public Guid Id
            {
                get;
                set;
            }
            public string FullName
            {
                get;
                set;
            }

        }
    }
    
    public class GroupsAsyncQueryResult
    {
        public PagedList<DTOs.Group> Groups
        {
            get;
            set;
        }
    }
    public class Handler: IRequestHandler<GroupsAsyncQuery, GroupsAsyncQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public Handler(IMapper mapper, DataContext dataContext)
        {
            _mapper      = mapper;
            _dataContext = dataContext;
        }

        public async Task<GroupsAsyncQueryResult> Handle(GroupsAsyncQuery request,
            CancellationToken cancellationToken)
        {
            // var groups = await _dataContext.Groups
            //     .Include(x => x.Payers)
            //     .ToListAsync(cancellationToken);
            
            var groupsQuery = _dataContext.Groups
                .Include(x => x.Payers)
                .ProjectTo<GetGroupsAsync.DTOs.Group>(_mapper.ConfigurationProvider)
                .AsNoTracking();
            
            var groups = await PagedList<GetGroupsAsync.DTOs.Group>.CreateAsync(groupsQuery, request.PageNumber, request.PageSize);
				
            if (groups == null)
            {
                throw new NoGroupsFoundExceptions();
            }

            return new GroupsAsyncQueryResult
            {
                Groups = groups
            };
        }
    }
   
}