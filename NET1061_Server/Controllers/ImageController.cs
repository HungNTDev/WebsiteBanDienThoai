using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        public ImageController(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        [HttpGet("{filename}")]
        public IActionResult GetImage(string filename)
        {
            var filePath = Path.Combine("F:\\CSharp\\CSharp6\\Project\\ASMC6\\NET1061_ASM\\NET1061_Server\\uploads", filename);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileExtension = Path.GetExtension(filename).ToLowerInvariant();
            string contentType;
            switch (fileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                default:
                    return BadRequest("Unsupported file type.");
            }
            var fileStream = new FileStream(filePath, FileMode.Open);
            return File(fileStream, contentType);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "product_items"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.StatusCode == HttpStatusCode.OK
                ? Ok(new { imageUrl = uploadResult.SecureUrl.ToString() })
                : BadRequest("Image upload failed.");
        }
    }
}
