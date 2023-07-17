using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;
using MineralWaterMonitoring.Common.Extension;
using MineralWaterMonitoring.Common.Pagination;

namespace MineralWaterMonitoring.Features.Contributions;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

   [HttpGet(Name = "GetContributionsAsync")]
    public async Task<ActionResult<GetContributionsAsync.GetContributionsAsyncResult>> GetContributionsAsync([FromQuery] UserParams userParams)
    {
                    try
                    {
                        var query = new GetContributionsAsync.GetContributionsAsyncQuery
                        {
                            PageSize = userParams.PageSize,
                            PageNumber = userParams.PageNumber
                        };
                
                        var contributions = await _mediator.Send(query);
                
                        Response.AddPaginationHeader(
                            contributions.CurrentPage,
                            contributions.PageSize,
                            contributions.TotalCount,
                            contributions.TotalPages,
                            contributions.HasPreviousPage,
                            contributions.HasNextPage);

                        var result = new
                        {
                            contributions,
                            contributions.CurrentPage,
                            contributions.PageSize,
                            contributions.TotalCount,
                            contributions.TotalPages,
                            contributions.HasPreviousPage,
                            contributions.HasNextPage
                        };
                                
                        return Ok(result); 
                    }
                    catch (Exception e)
                    { 
                        var response = new { Success = false, Messages = new List<string> { e.Message } };
                        return Conflict(response);
                    }
                    
    }
}