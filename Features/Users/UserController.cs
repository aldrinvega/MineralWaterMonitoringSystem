using MediatR;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;

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
           response.Data = response;
           response.Messages.Add("User Added Successfully");
           return Ok(response);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet(Name = "GetAllUsers")]
    public async Task<ActionResult<IEnumerable<GetUsersAsync.UsersAsyncQueryResult>>>
        GetAllUsersAsync()
    {
        var response = new QueryOrCommandResult<IEnumerable<GetUsersAsync.UsersAsyncQueryResult>>();
        try
        {
            var query = new GetUsersAsync.UsersAsyncQuery();
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