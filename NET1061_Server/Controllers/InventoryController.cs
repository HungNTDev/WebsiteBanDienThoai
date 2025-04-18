namespace NET1061_Server.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class InventoryController : ControllerBase
    //{
    //    private readonly IMediator _mediator;

    //    public InventoryController(IMediator mediator)
    //    {
    //        _mediator = mediator;
    //    }

    //    [HttpGet("getAll")]
    //    public async Task<IActionResult> GetAllInventories([FromQuery] Filter filter)
    //    {
    //        var request = new GetAll_InventoryQuery(filter);
    //        var result = await _mediator.Send(request);
    //        if (result != null)
    //        {
    //            return Ok(result);
    //        }
    //        return BadRequest(result);
    //    }

    //    [HttpGet("getDetail")]
    //    public async Task<IActionResult> GetInventoryDetail([FromQuery] Guid id)
    //    {
    //        var request = new GetDetail_InventoryQuery(id);
    //        var result = await _mediator.Send(request);
    //        return result.Match(
    //            apiResponse => StatusCode(apiResponse.StatusCode, apiResponse),
    //            dto => Ok(dto)
    //        );
    //    }

    //    [HttpPost("create")]
    //    public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDto command)
    //    {
    //        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
    //        if (string.IsNullOrEmpty(userName))
    //        {
    //            return Unauthorized("User is not authenticated.");
    //        }

    //        var request = new CreateInventoryCommand(command, userName);
    //        var result = await _mediator.Send(request);
    //        if (result != null)
    //        {
    //            return Ok(result);
    //        }
    //        return BadRequest(result);
    //    }

    //    [HttpPut("update")]
    //    public async Task<IActionResult> UpdateInventory([FromBody] UpdateInventoryDto command)
    //    {
    //        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
    //        if (string.IsNullOrEmpty(userName))
    //        {
    //            return Unauthorized("User is not authenticated.");
    //        }
    //        var request = new UpdateInventoryCommand(command, userName);
    //        var result = await _mediator.Send(request);
    //        if (result != null)
    //        {
    //            return Ok(result);
    //        }
    //        return BadRequest(result);
    //    }
    //}
}
