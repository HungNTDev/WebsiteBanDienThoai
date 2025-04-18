using Application.OrderManagement.Commands.Create;
using Application.OrderManagement.Queries.GetOrderById;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
