using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Role.Exceptions;

namespace MineralWaterMonitoring.Features.Role;

public class GetRoleAsync
{
    public class RoleAsyncQuery : IRequest<RolesAsyncResult>
    {}

    public class DTO
    {
        public class Roles
        {
            public Guid Id { get; set; }
            public string RoleName { get; set; }
            public ICollection<DTO.Users> Users { get; set; }
        }

        public class Users
        {
            public Guid UserId { get; set; }
            public string Fullname { get; set; }
        }
    }

    public class RolesAsyncResult
        {
            public IEnumerable<DTO.Roles> Roles { get; set; }
        }
        
        public class Handler : IRequestHandler<RoleAsyncQuery, RolesAsyncResult>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<RolesAsyncResult> Handle(RoleAsyncQuery request, CancellationToken cancellationToken)
            {
                var role = await _context.Roles
                    .Include(x => x.Users)
                    .ToListAsync(cancellationToken);

                if (role == null)
                {
                    throw new NoRolesFoundException();
                }

                var roles = new RolesAsyncResult()
                {
                    Roles = _mapper.Map<IEnumerable<DTO.Roles>>(role)
                };

                return roles;
            }
        }
    
}