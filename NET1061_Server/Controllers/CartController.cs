using Application.CartManagement.Commands.AddCart;
using Application.CartManagement.Commands.DeleteCart;
using Application.CartManagement.Commands.DeleteCartItem;
using Application.CartManagement.Commands.UpdateCart;
using Application.CartManagement.Queries.GetCarByUser;
using Application.CartManagement.Queries.GetCartById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            var request = new GetCartByIdQuery(id);
            var result = await _mediator.Send(request);
            if (result.IsT1)
            {
                return NotFound(result.AsT1);
            }
            return Ok(result.AsT0);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyCart([FromQuery] Guid userId)
        {
            var result = await _mediator.Send(new GetCartByUserQuery(userId));
            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCartDto model)
        {
            var request = new CreateCartCommand(model);
            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCartDto model)
        {
            var request = new UpdateCartCommand(model);
            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart([FromBody] DeleteCartDto model)
        {
            var result = await _mediator.Send(new DeleteCartCommand(model));
            return Ok(result);
        }

        // Xoá một sản phẩm khỏi giỏ hàng
        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteCartItem([FromBody] DeleteCartItemDto model)
        {
            var result = await _mediator.Send(new DeleteCartItemCommand(model));
            return Ok(result);
        }
    }
}
