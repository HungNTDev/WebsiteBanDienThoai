using Application.Abstract.Library;
using Application.PaymentManagement.ModelVnPay;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Application.Abstract.Services
{
    public interface IVnPayService
    {
        Task<string> GeneratePaymentUrl(Guid orderId, decimal amount);
        VnPayCallbackResult VerifyPayment(IQueryCollection queryParams);
        VnPayCallbackResult VerifyPayment(Dictionary<string, string> queryParams);
    }
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public VnPayService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GeneratePaymentUrl(Guid orderId, decimal amount)
        {
            var url = _config["VnPay:BaseUrl"];
            var tmnCode = _config["VnPay:TmnCode"];
            var hashSecret = _config["VnPay:HashSecret"];
            var returnUrl = _config["VnPay:ReturnUrl"];

            var pay = new VnPayLibrary();
            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", tmnCode);
            var total = Convert.ToInt64(amount * 100);
            pay.AddRequestData("vnp_Amount", total.ToString());
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", "TestPayment");
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", returnUrl);
            pay.AddRequestData("vnp_TxnRef", orderId.ToString());

            Console.WriteLine("HasSecret: " + hashSecret);

            var paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            Console.WriteLine("✅ Full Payment URL:");
            Console.WriteLine(paymentUrl);

            return paymentUrl;
        }

        public VnPayCallbackResult VerifyPayment(IQueryCollection queryParams)
        {
            var hashSecret = _config["VnPay:HashSecret"];

            var isValid = new VnPayLibrary().ValidateSignature(queryParams, hashSecret);
            var responseCode = queryParams["vnp_ResponseCode"].ToString();
            var status = responseCode == "00" ? PaymentStatus.Success : PaymentStatus.Failed;

            return new VnPayCallbackResult
            {
                IsSuccess = isValid,
                Status = status,
                OrderId = Guid.TryParse(queryParams["vnp_TxnRef"], out var orderId) ? orderId : Guid.Empty,
                TransactionId = queryParams["vnp_TransactionNo"],
                Amount = decimal.TryParse(queryParams["vnp_Amount"], out var amount)
                    ? amount / 100 : 0
            };
        }

        public VnPayCallbackResult VerifyPayment(Dictionary<string, string> queryParams)
        {
            var queryCollection = new QueryCollection(queryParams.ToDictionary(
                x => x.Key,
                x => new StringValues(x.Value)
            ));
            return VerifyPayment(queryCollection);
        }
    }
}
