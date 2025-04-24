using Application.OrderManagement.Commands.Create;
using Application.OrderManagement.Queries.GetDaily;
using Application.OrderManagement.Queries.GetOrderById;
using Application.OrderManagement.Queries.GetOrderHistory;
using Application.OrderManagement.Queries.GetOrdersByUserIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(orderId));
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(Guid userId)
        {
            var result = await _mediator.Send(new GetOrdersByUserIdQuery(userId));
            return Ok(result);
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyStats([FromQuery] int days = 7)
        {
            var result = await _mediator.Send(new GetDailyOrderStatsQuery { NumberOfDays = days });
            return Ok(result);
        }

        [HttpGet("user/history")]

        public async Task<IActionResult> GetUserOrderHistory()
        {
            var response = new GetOrderHistoryQuery();
            var result = await _mediator.Send(response);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
