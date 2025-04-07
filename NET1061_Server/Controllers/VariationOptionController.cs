using Application.Abstract.BaseClass;
using Application.VariationOptionManagement.Commands.Create;
using Application.VariationOptionManagement.Commands.Update;
using Application.VariationOptionManagement.Queries.GetAll;
using Application.VariationOptionManagement.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationOptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VariationOptionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetVariationOptions([FromQuery] Filter filter)
        {
            var request = new GetAllVariationOptionQuery(filter);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariationOptionById(Guid id)
        {
            var request = new GetVariationOptionByIdQuery(id);
            var result = await _mediator.Send(request);
            return result.Match(
                apiResponse => StatusCode(apiResponse.StatusCode, apiResponse),
                dto => Ok(dto)
            );
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVariationOption([FromBody] CreateVariationOptionDto command)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
            var request = new CreateVariationOptionCommand(command, userName);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateVariationOption(Guid id,
            [FromBody] UpdateVariationOptionDto command)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
            command.Id = id;
            var request = new UpdateVariationOptionCommand(command, userName);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
