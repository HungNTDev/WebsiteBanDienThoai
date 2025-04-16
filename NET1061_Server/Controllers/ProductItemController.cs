using Application.Abstract.BaseClass;
using Application.ProductItemManagement.Commands.Create;
using Application.ProductItemManagement.Commands.Update;
using Application.ProductItemManagement.Queries.GetAll;
using Application.ProductItemManagement.Queries.GetById;
using Application.ProductItemManagement.Queries.GetByOptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        public IMediator _mediator;
        public ProductItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProductItems([FromQuery] Filter query)
        {
            var request = new GetAllProductItemQuery(query);
            var result = await _mediator.Send(request);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductItemById(Guid id)
        {
            var request = new GetProductItemByIdQuery(id);
            var result = await _mediator.Send(request);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("by-options")]
        public async Task<IActionResult> GetByOptions([FromBody] GetProductItemQuery query)
        {
            var request = new GetProductItemQuery(query.ProductId, query.OptionIds);
            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductItemDto model)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }

            var request = new CreateProductItemCommand(model, userName);
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Updte(Guid id, [FromBody] UpdateProductItemDto model)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
            model.Id = id;
            var request = new UpdateProductItemCommand(model, userName);
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
