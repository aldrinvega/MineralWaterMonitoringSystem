using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;

namespace MineralWaterMonitoring.Features.Collections;

[Route("api/[controller]")]
[ApiController]
public class CollectionController : BaseController
{
    public CollectionController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
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
    public async Task<IActionResult> GetCollectionsAsync()
    {
        return await Handle<IEnumerable<GetCollectionsAsync.GetCollectionsAsyncQueryResult>>(
            new GetCollectionsAsync.GetCollectionsAsyncQuery());
    }
}