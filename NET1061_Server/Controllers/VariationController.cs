using Application.Abstract.BaseClass;
using Application.VariationManagement.Commands.Create;
using Application.VariationManagement.Commands.Update;
using Application.VariationManagement.Queries.GetAll;
using Application.VariationManagement.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> CreateVariation([FromBody] CreateVariationDto command)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
            var request = new CreateVariationCommand(command, userName);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateVariation(Guid id, [FromBody] UpdateVariationDto command)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
            command.Id = id;
            var request = new UpdateVariationCommand(command, userName);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
