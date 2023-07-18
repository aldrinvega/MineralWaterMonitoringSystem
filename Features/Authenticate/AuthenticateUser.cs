using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MineralWaterMonitoring.Data;
using MineralWaterMonitoring.Features.Users.Exceptions;

namespace MineralWaterMonitoring.Features.Authenticate;

public abstract class AuthenticateUser
{
    public class AuthenticateUserQuery : IRequest<AuthenticateUserResult>
    {
        public AuthenticateUserQuery(string password, string username)
        {
            Password = password;
            Username = username;
        }

        [Required]
        public string Username
        {
            get;
        }
        [Required]
        public string Password
        {
            get;
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
                var results = _mapper.Map<AuthenticateUserResult>(result);
                return Task.FromResult(results);
            }

            private string GenerateJwtToken(Domain.Users user)
            {
                var key = _configuration.GetValue<string>("JwtConfig:Key");
                var audience = _configuration.GetValue<string>("JwtConfig:Audience");
                var issuer = _configuration.GetValue<string>("JwtConfig:Issuer");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("id", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(keyBytes), 
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}