using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MineralWaterMonitoring.Features.Collections;
[Route("api/[controller]")]
[ApiController]
public class CollectionControllers : ControllerBase
{
   private readonly IMediator _mediator;

   public CollectionControllers(IMediator mediator)
   {
      _mediator = mediator;
   }
}