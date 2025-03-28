using Microsoft.AspNetCore.Http;

namespace Application.Abstract.Repository.Base
{
    public interface IUploadHelper
    {
        Task UploadImage(IFormFile file, string rootPath, string phanloai);
        Task RemoveImage(string filePath);
    }
}
