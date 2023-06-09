using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Group.Execptions;

namespace MineralWaterMonitoring.Features.Group;

public class GetGroupsAsync
{
    public class GroupsAsyncQuery: IRequest<GroupsAsyncQueryResult> {}
	
	
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
			
            public ICollection<DTOs.User> User
            {
                get;
                set;
            }
        }
		
        public class User
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
        public IEnumerable<DTOs.Group> Groups
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
            var groups = await _dataContext.Groups
                .Include(x => x.Payers)
                .ToListAsync(cancellationToken);
				
            if (groups == null)
            {
                throw new NoGroupsFoundExceptions();
            }

            var result = new GroupsAsyncQueryResult
            {
                Groups = _mapper.Map<IEnumerable<DTOs.Group>>(groups)
            };
            
            return result;
        }
    }
   
}