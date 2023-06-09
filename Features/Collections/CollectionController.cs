using MediatR;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;

namespace MineralWaterMonitoring.Features.Collections;

[Route("api/[controller]")]
[ApiController]

public class CollectionController : ControllerBase
{
    private readonly IMediator _mediator;

    public CollectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateNewCollection")]
    public async Task<IActionResult> CreateNewCollection(AddNewCollection.AddNewCollectionCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok("Collection successfully created");
        }
        catch (Exception e)
        {
            return Conflict(new
            {
                e.Message
            });
        }
    }

    [HttpGet(Name = "GetCollectionAsync")]
    public async Task<ActionResult<IEnumerable<GetCollectionsAsync.GetCollectionsAsyncQueryResult>>>
        GetCollectionsAsync()
    {
        var response = new QueryOrCommandResult<IEnumerable<GetCollectionsAsync.GetCollectionsAsyncQueryResult>>();
        try
        {
            var query = new GetCollectionsAsync.GetCollectionsAsyncQuery();
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