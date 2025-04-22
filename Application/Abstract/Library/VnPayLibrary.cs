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

            // ✅ Tạo SignData để hash
            var signData = string.Join("&", ordered.Select(kvp =>
                $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            Console.WriteLine("🔍 SignData = " + signData);

            // ✅ Tính SecureHash
            string secureHash = ComputeHash(hashSecret, signData);
            Console.WriteLine("🔐 SecureHash = " + secureHash);

            // ✅ Query string với value được encode
            var query = string.Join("&", ordered.Select(kvp =>
                $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value, Encoding.UTF8)}"));

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

        public static string ComputeHash(string key, string data)
        {
            var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));

            // ✅ Chuyển thành HEX UPPERCASE, không có dấu, không có dấu cộng hay bằng
            return BitConverter.ToString(hash).Replace("-", "").ToUpper();
        }

    }
}
