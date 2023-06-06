using MediatR;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;
using NuGet.Protocol;

namespace MineralWaterMonitoring.Features.Group;

[Route("api/[controller]")]
[ApiController]

public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewGroup")]
    public async Task<ActionResult<QueryOrCommandResult<object>>> AddNewGroup(AddNewGroup.AddNewGroupCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var result = await _mediator.Send(command);
            response.Data = result;
            response.Success = true;
            response.Messages.Add("Group added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Success = false;
            return Conflict(response);
        }
    }

    [HttpGet(Name = "GetGroups")]
    public async Task<ActionResult<IEnumerable<GetGroupsAsync.GroupsAsyncQueryResult>>>
        GetGroups()
    {
        var response = new QueryOrCommandResult<IEnumerable<GetGroupsAsync.GroupsAsyncQueryResult>>();
        try
        {
            var query = new GetGroupsAsync.GroupsAsyncQuery();
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