using Application.Abstract;
using Application.VariationManagement.Commands.Create;
using Application.VariationManagement.Commands.Update;
using Application.VariationManagement.Queries.GetAll;
using Application.VariationManagement.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationController : ControllerBase
    {
        private IMediator _mediator;

        public VariationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetVariation([FromQuery] Filter filter)
        {
            var request = new GetAllVariationQuery(filter);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariationById(Guid id)
        {
            var request = new GetVariationByIdQuery(id);
            var result = await _mediator.Send(request);
            return result.Match(
                apiResponse => StatusCode(apiResponse.StatusCode, apiResponse),
                dto => Ok(dto)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateVariation([FromBody] CreateVariationDto command)
        {
            var request = new CreateVariationCommand(command);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVariation(Guid id, [FromBody] UpdateVariationDto command)
        {
            command.Id = id;
            var request = new UpdateVariationCommand(command);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
