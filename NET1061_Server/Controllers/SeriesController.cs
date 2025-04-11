using Application.Abstract.BaseClass;
using Application.SeriesManagement.Commands.Create;
using Application.SeriesManagement.Commands.Update;
using Application.SeriesManagement.Queries.GetAll;
using Application.SeriesManagement.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] Filter query)
        {
            var request = new GetAllSeriesQuery(query);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("getDetail")]
        public async Task<IActionResult> GetDetail([FromQuery] Guid id)
        {
            var request = new GetDetailSeriesQuery(id);
            var result = await _mediator.Send(request);
            return result.Match(
                apiResponse => StatusCode(apiResponse.StatusCode, apiResponse),
                dto => Ok(dto)
            );
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateSeriesDto command)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
            var request = new CreateSeriesCommand(command, userName);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSeriesDto command)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
            command.Id = id;
            var request = new UpdateSeriesCommand(command, userName);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
