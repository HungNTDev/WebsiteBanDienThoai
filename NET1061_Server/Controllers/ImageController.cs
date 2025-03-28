using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpGet("{filename}")]
        public IActionResult GetImage(string filename)
        {
            var filePath = Path.Combine("F:\\CSharp\\CSharp6\\Project\\PS38090_NguyenTuanHung_ASM\\NET1061_Assignment\\NET1061_Server\\uploads", filename);
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
    }
}
