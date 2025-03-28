using Application.Abstract.Repository.Base;
using Microsoft.AspNetCore.Http;

namespace Repositories.Repository.BaseRepository
{
    public class UploadHelper : IUploadHelper
    {
        public async Task UploadImage(IFormFile file, string rootPath, string phanloai)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty", nameof(file));
            }

            // Tạo thư mục gốc nếu nó không tồn tại  
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            // Tạo đường dẫn cho phân loại  
            string dirPath = Path.Combine(rootPath, phanloai);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            // Tạo đường dẫn cho tệp  
            string filePath = Path.Combine(dirPath, file.FileName);

            // Kiểm tra xem tệp đã tồn tại chưa  
            if (!File.Exists(filePath))
            {
                // Sử dụng using để đảm bảo Stream được đóng sau khi sử dụng  
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream); // Sử dụng hàm bất đồng bộ để sao chép  
                }
            }
            else
            {
                throw new IOException($"File {file.FileName} already exists.");
            }
        }

        public async Task RemoveImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await Task.CompletedTask; // Phương thức không trả về giá trị cụ thể nào  
        }
    }
}
