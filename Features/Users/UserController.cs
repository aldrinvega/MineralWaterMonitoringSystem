using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;
using MineralWaterMonitoring.Common.Extension;
using MineralWaterMonitoring.Common.Pagination;

namespace MineralWaterMonitoring.Features.Users;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewUser")]
    public async Task<ActionResult<QueryOrCommandResult<object>>> AddNewUser(AddNewUser.AddNewUserCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
           var result = await _mediator.Send(command);
           response.Success = true;
           response.Data = result;
           response.Messages.Add("User Added Successfully");
           return Ok(response);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserParams userParams)
    {
        try
        {
            var query = new GetUsersAsync.UsersAsyncQuery
            {
                PageNumber = userParams.PageNumber,
                PageSize = userParams.PageSize
            };
            var users = await _mediator.Send(query);

            Response.AddPaginationHeader(users.CurrentPage, users.TotalCount, users.TotalPages, users.PageSize, users.HasNextPage, users.HasPreviousPage);

            var result = new
            {
                users,
                users.PageSize,
                users.CurrentPage,
                users.TotalCount,
                users.TotalPages,
                users.HasPreviousPage,
                users.HasNextPage
            };

            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(new { message = e.Message });
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUserInformation([FromBody] UpdateUserInformation.UpdateUserInformationCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok($"Information of {command.Fullname} is successfully updated");
        }
        catch (Exception e)
        {
            return Conflict(new
            {
                e.Message
            });
        }
    }

    [HttpPatch("UpdateUserStatus")]
    public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateUserStatus.UpdateUserStatusCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok($"User successfully {command.Status}");
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