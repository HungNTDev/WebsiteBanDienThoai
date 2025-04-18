using User.Models.Authentication;

namespace User.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public object Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class LoginRequest
    {
        public LoginModel Model { get; set; }
    }
}
