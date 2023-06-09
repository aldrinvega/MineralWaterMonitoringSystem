using MediatR;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;

namespace MineralWaterMonitoring.Features.Contributions;

[Route("api/[controller]")]
[ApiController]

public class ContributionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContributionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddContribution")]
    public async Task<IActionResult> AddContribution(AddContribution.AddContributionCommand command)
    {
        var response = new QueryOrCommandResult<Unit>();
        try
        {
            var result = await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Contribution is made successfully");
            return CreatedAtRoute("GetContributionsAsync", result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet(Name = "GetContributionsAsync")]
    public async Task<ActionResult<GetContributionsAsync.GetContributionsAsyncResult>> GetContributionsAsync()
    {
        var response = new QueryOrCommandResult<IEnumerable<GetContributionsAsync.GetContributionsAsyncResult>>();
        try
        {
            var query = new GetContributionsAsync.GetContributionsAsyncQuery();
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