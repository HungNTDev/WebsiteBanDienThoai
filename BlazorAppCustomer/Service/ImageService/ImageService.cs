namespace BlazorAppCustomer.Service.ImageService
{
    public class ImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GetImageUrl(string filename)
        {
            var imageurl = $"https://localhost:7026/api/Image/{filename}";
            return imageurl;
        }
    }
}
