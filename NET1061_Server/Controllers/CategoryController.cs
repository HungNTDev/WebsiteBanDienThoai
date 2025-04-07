using Application.Abstract.BaseClass;
using Application.Abstract.Repository.Base;
using Application.CategoryManagement.Commands.Create;
using Application.CategoryManagement.Commands.Update;
using Application.CategoryManagement.Queries.GetAll;
using Application.CategoryManagement.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUploadHelper _uploadHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(IMediator mediator,
                                  IUploadHelper uploadHelper,
                                  IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _uploadHelper = uploadHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(
            [FromQuery] Filter filter)
        {
            var request = new GetAllCategoriesQuery(filter);
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriesById(Guid id)
        {
            var request = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(request);
            return result.Match(
                apiResponse => StatusCode(apiResponse.StatusCode, apiResponse),
                dto => Ok(dto)
            );
        }

        // POST api/<CategoriesController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateCategoryDto model)
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

            if (model.FromFileImages != null && model.FromFileImages.Length > 0)
            {
                var uploadsFolder = Path.Combine(contentRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, model.FromFileImages.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.FromFileImages.CopyToAsync(stream);
                }

                model.Image = model.FromFileImages.FileName;
            }


            var request = new CreateCategoryCommand(model, userName);
            var result = await _mediator.Send(request);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // PUT api/<CategoriesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] UpdateCategoryDto model)
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

            if (model.FromFileImages != null && model.FromFileImages.Length > 0)
            {
                var uploadsFolder = Path.Combine(contentRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, model.FromFileImages.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.FromFileImages.CopyToAsync(stream);
                }

                model.Image = model.FromFileImages.FileName;
            }
            model.Id = id;

            var request = new UpdateCategoryCommand(model, userName);
            var result = await _mediator.Send(request);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
