using Application.Abstract.BaseClass;
using Application.BrandManagement.Commands.Create;
using Application.BrandManagement.Commands.Update;
using Application.BrandManagement.Queries.GetAll;
using Application.BrandManagement.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BrandController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Filter query)
        {
            var request = new GetAllBrandQuery(query);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetDetailBrandQuery(id);
            var result = await _mediator.Send(request);
            return result.Match(
               apiResponse => StatusCode(apiResponse.StatusCode, apiResponse),
               dto => Ok(dto)
           );
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreateBrandDto model)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
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
            var command = new CreateBrandCommand(model, userName);
            var result = await _mediator.Send(command);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateBrandDto model)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated.");
            }
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
            model.Id = id;
            var command = new UpdateBrandCommand(model, userName);
            var result = await _mediator.Send(command);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
