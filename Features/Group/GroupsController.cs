using MediatR;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;
using MineralWaterMonitoring.Common.Extension;
using MineralWaterMonitoring.Common.Pagination;
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
    public async Task<ActionResult> GetGroups([FromQuery] UserParams userParams)
    {
        try
        {
            var query = new GetGroupsAsync.GroupsAsyncQuery
            {
                PageNumber = userParams.PageNumber,
                PageSize = userParams.PageSize
            };
            var groups = await _mediator.Send(query);

            Response.AddPaginationHeader(
                groups.Groups.CurrentPage,
                groups.Groups.TotalCount,
                groups.Groups.TotalPages,
                groups.Groups.PageSize,
                groups.Groups.HasNextPage,
                groups.Groups.HasPreviousPage
            );
            var result = new
            {
                groups = groups.Groups,
                pageSize = groups.Groups.PageSize,
                currentPage = groups.Groups.CurrentPage,
                totalCount = groups.Groups.TotalCount,
                totalPages = groups.Groups.TotalPages,
                hasPreviousPage = groups.Groups.HasPreviousPage,
                hasNextPage = groups.Groups.HasNextPage
            };
            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(new { message = e.Message });
        }
    }
}