using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Authenticate(AuthenticateUser.AuthenticateUserQuery request)
    {
        try
        {
            var query = new AuthenticateUser.AuthenticateUserQuery();
            var result = _mediator.Send(request);
            return Ok(result);
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