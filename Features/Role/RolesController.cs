using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MineralWaterMonitoring.Common;
using MineralWaterMonitoring.Features.Role.Exceptions;

namespace MineralWaterMonitoring.Features.Role;

[Route("api/[controller]")]
[ApiController]

public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewRole")]
    public async Task<ActionResult> AddNewRoles(AddNewRole.AddNewRoleCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Role successfully added");
        }
        catch (RoleAlreadyExistException e)
        {
            return Conflict(new
            {
                e.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<GetRoleAsync.RolesAsyncResult>> GetRoleAsync()
    {
        var response = new QueryOrCommandResult<GetRoleAsync.RolesAsyncResult>();
        try
        {
            var query = new GetRoleAsync.RoleAsyncQuery();
            var result = await _mediator.Send(query);

            response.Success = true;
            response.Data = result;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
    
}