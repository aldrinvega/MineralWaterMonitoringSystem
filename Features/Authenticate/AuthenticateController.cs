using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;

namespace MineralWaterMonitoring.Features.Authenticate;
[Route("api/[controller]")]
[ApiController]

public class AuthenticateController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<ActionResult<AuthenticateUser.AuthenticateUserResult>> Authenticate(AuthenticateUser.AuthenticateUserQuery request)
    {

        var response = new QueryOrCommandResult<AuthenticateUser.AuthenticateUserResult>();
        try
        {
            // var query = new AuthenticateUser.AuthenticateUserQuery();
            var result = await _mediator.Send(request);
            response.Data = result;
            return Ok(response);
        }
        catch (Exception e)
        {
            return Conflict(new
            {
                e.Message
            });
        }
    }
}