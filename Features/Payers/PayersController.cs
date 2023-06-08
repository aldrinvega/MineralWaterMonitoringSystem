using MediatR;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;

namespace MineralWaterMonitoring.Features.Payers;

[Route("api/[controller]")]
[ApiController]

public class PayersController : ControllerBase
{
   private readonly IMediator _mediator;

   public PayersController(IMediator mediator)
   {
      _mediator = mediator;
   }
   [HttpGet(Name = "GetAllPayer")]
   public async Task<ActionResult<GetPayersAsync.GetPayersAsyncQueryResult>>
      GetAllPayers()
   {
      var response = new QueryOrCommandResult<IEnumerable<GetPayersAsync.GetPayersAsyncQueryResult>>();
      try
      {
         var query = new GetPayersAsync.GetPayersAsyncQuery();
         var result = await _mediator.Send(query);
         response.Success = true;
         response.Data = result;
         return Ok(response);

      }
      catch (Exception e)
      {
         response.Data = null;
         response.Success = false;
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }
   
   [HttpPost (Name = "AddNewPayer")]
   public async Task<IActionResult> AddNewPayer(AddNewPayer.AddNewPayerCommand command)
   {
      try
      {
        var result = await _mediator.Send(command);
         return CreatedAtRoute("GetAllPayer", result);
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