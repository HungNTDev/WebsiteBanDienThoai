namespace Admin.Services
{
    public interface IImageService
    {
        string GetImageUrl(string filename);
    }

    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GetImageUrl(string filename)
        {
            var imageUrl = $"https://localhost:7026/api/Image/{filename}";
            return imageUrl; // Trả về URL để sử dụng trong img tag  
        }
    }
}
