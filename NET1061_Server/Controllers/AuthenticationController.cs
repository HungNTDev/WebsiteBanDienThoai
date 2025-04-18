using Application.AuthenticationManagement.Commands.EditProfile;
using Application.AuthenticationManagement.Commands.ForgotPassword;
using Application.AuthenticationManagement.Commands.Google;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
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

        [HttpPost("googlelogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
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
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand model)
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

        [HttpPut("editprofile")]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileDto model)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentRootPath))
            {
                return BadRequest("ContentRootPath is not configured properly.");
            }

            if (model.formFile != null && model.formFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(contentRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, model.formFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.formFile.CopyToAsync(stream);
                }

                model.Image = model.formFile.FileName;
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            var command = new EditProfileCommand(model, userId);
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
