using Application.Abstract.BaseClass;
using Application.Abstract.Repository.Base;
using Application.ProductManagement.Commands.Create;
using Application.ProductManagement.Commands.Update;
using Application.ProductManagement.Queries.GetAll;
using Application.ProductManagement.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUploadHelper _uploadHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment,
                                 IUploadHelper uploadHelper,
                                 IMediator mediator)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadHelper = uploadHelper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] Filter query)
        {
            var request = new GetAllProductQuery(query);
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return NotFound("No products found.");
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var request = new GetDetailProductQuery(id);
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateProductDto model)
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

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(contentRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, model.ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                model.Image = model.ImageFile.FileName;
            }

            var request = new CreateProductCommand(model, userName);
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, [FromForm] UpdateProductDto model)
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

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(contentRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, model.ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                model.Image = model.ImageFile.FileName;
            }
            model.Id = id;
            var request = new UpdateProductCommand(model, userName);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
