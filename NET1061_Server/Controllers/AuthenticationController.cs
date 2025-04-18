using Application.AuthenticationManagement.Commands.ForgotPassword;
using Application.AuthenticationManagement.Commands.Login;
using Application.AuthenticationManagement.Commands.Register;
using Application.AuthenticationManagement.Commands.ResetPassword;
using Application.AuthenticationManagement.Queries.Profile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]
        ForgotPasswordCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }


            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                return BadRequest(new { Message = "Invalid User ID format" });
            }

            var request = new GetProfileQuery(userGuid);
            var result = await _mediator.Send(request);
            return result.Match(
                apiResponse => StatusCode(apiResponse.StatusCode, apiResponse),
                dto => Ok(dto)
            );
        }
    }
}
