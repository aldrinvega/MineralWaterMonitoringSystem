﻿using System.Runtime.CompilerServices;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Group.Execptions;
using MineralWaterMonitoring.Features.Users;

namespace MineralWaterMonitoring.Features.Group;

public class GetGroupsAsync
{
    public class GroupsAsyncQuery : IRequest<IEnumerable<GroupsAsyncQueryResult>> {}

    public class GroupsAsyncQueryResult
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
        public Domain.Users Users
        {
            get;
            set;
        }

    }
    public class Handler : IRequestHandler<GroupsAsyncQuery, IEnumerable<GroupsAsyncQueryResult>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public Handler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<GroupsAsyncQueryResult>> Handle(GroupsAsyncQuery request,
            CancellationToken cancellationToken)
        {
            var groups = await _dataContext.Groups
                .Include(x => x.UsersCollection)
                .ToListAsync(cancellationToken);
            if (groups == null)
            {
                throw new NoGroupsFoundExceptions();
            }

            var result = _mapper.Map<IEnumerable<GroupsAsyncQueryResult>>(groups);
            return result;
        }
    }
   
}