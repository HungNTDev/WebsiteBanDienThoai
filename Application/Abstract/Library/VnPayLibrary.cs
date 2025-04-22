using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Application.Abstract.Library
{
    public class VnPayLibrary
    {
        private readonly SortedList<string, string> _requestData = new();

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                _requestData[key] = value;
        }

        public string CreateRequestUrl(string baseUrl, string hashSecret)
        {
            var ordered = _requestData.OrderBy(kvp => kvp.Key);

            // ✅ URL encode giá trị cho query string
            var query = string.Join("&", ordered.Select(kvp =>
                $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            // ✅ URL encode giá trị trong SignData nếu VNPAY yêu cầu (tránh lỗi code 70)
            var signData = string.Join("&", ordered.Select(kvp =>
                $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));  // <-- Bây giờ cũng encode ở đây

            Console.WriteLine("🔍 SignData = " + signData);
            string secureHash = ComputeHash(hashSecret, signData);
            Console.WriteLine("🔐 SecureHash = " + secureHash);

            return $"{baseUrl}?{query}&vnp_SecureHash={secureHash}";
        }



        public bool ValidateSignature(IQueryCollection query, string hashSecret)
        {
            var data = query
                .Where(kvp => kvp.Key.StartsWith("vnp_") && kvp.Key != "vnp_SecureHash" && kvp.Key != "vnp_SecureHashType")
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            // ❌ KHÔNG ENCODE ở đây
            string signData = string.Join("&", data
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => $"{kvp.Key}={kvp.Value}")); // Raw value

            string checkHash = ComputeHash(hashSecret, signData);

            return checkHash.Equals(query["vnp_SecureHash"], StringComparison.OrdinalIgnoreCase);
        }



        private string ComputeHash(string key, string data)
        {
            var encoding = new UTF8Encoding(false); // Không BOM
            var hmac = new HMACSHA512(encoding.GetBytes(key));
            var normalizedData = data.Normalize(NormalizationForm.FormC); // Chuẩn Unicode
            var hashValue = hmac.ComputeHash(encoding.GetBytes(normalizedData));
            return BitConverter.ToString(hashValue).Replace("-", "").ToUpper();
        }
    }
}
