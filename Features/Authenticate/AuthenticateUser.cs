using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Users.Exceptions;
using NuGet.Protocol;

namespace MineralWaterMonitoring.Features.Authenticate;

public class AuthenticateUser
{
    public class AuthenticateUserQuery : IRequest<AuthenticateUserResult>
    {
        [Required]
        public string Username
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
    }

    public class AuthenticateUserResult
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Fullname
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public Guid GroupId
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
        public string Token
        {
            get;
            set;
        }

        public AuthenticateUserResult(Domain.Users user, string token)
        {
            Id = user.Id;
            Fullname = user.FullName;
            Username = user.UserName;
            Password = user.Password;
            Token = token;
        }

        public class Handler : IRequestHandler<AuthenticateUserQuery, AuthenticateUserResult>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IConfiguration configuration, IMapper mapper)
            {
                _context = context;
                _configuration = configuration;
                _mapper = mapper;
            }

            public Task<AuthenticateUserResult> Handle(AuthenticateUserQuery command,
                CancellationToken cancellationToken)
            {
                var user = _context.Users.SingleOrDefault(x => x.UserName == command.Username
                    && x.Password == command.Password);
                if (user == null)
                    throw new NoUsersFoundException();
                var token = GenerateJwtToken(user);
                var result = new AuthenticateUserResult(user, token);
                var results = _mapper.Map<AuthenticateUserResult>(result) ;
                return Task.FromResult(results);
            }

            private string GenerateJwtToken(Domain.Users user)
            {
                var key = _configuration.GetValue<string>("JwtConfig:Key");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}