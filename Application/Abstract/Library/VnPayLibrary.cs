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

            // ✅ Encode đúng chuẩn cho SignData
            var signData = string.Join("&", ordered.Select(kvp =>
                $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value, Encoding.UTF8)}"));

            Console.WriteLine("🔍 SignData = " + signData);

            string secureHash = ComputeHash(hashSecret, signData);
            Console.WriteLine("🔐 SecureHash = " + secureHash);

            // ✅ Dùng lại signData làm query string vì đã encode đúng chuẩn
            var query = signData;

            return $"{baseUrl}?{query}&vnp_SecureHash={secureHash}";
        }

        public bool ValidateSignature(IQueryCollection query, string hashSecret)
        {
            var data = query
                .Where(kvp => kvp.Key.StartsWith("vnp_") && kvp.Key != "vnp_SecureHash" && kvp.Key != "vnp_SecureHashType")
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            string signData = string.Join("&", data.Select(kvp =>
                $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value, Encoding.UTF8)}"));

            string checkHash = ComputeHash(hashSecret, signData);
            Console.WriteLine("CheckHash= " + checkHash);
            Console.WriteLine("Received Hash= " + query["vnp_SecureHash"]);

            return checkHash.Equals(query["vnp_SecureHash"], StringComparison.OrdinalIgnoreCase);
        }

        public static string ComputeHash(string key, string data)
        {
            var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToUpper();
        }

        public string GetQueryString()
        {
            var sorted = _requestData
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .OrderBy(kvp => kvp.Key);

            var query = sorted.Select(kvp => $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value, Encoding.UTF8)}");
            return string.Join("&", query);
        }
    }
}
