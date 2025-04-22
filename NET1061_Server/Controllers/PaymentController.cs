using Application.Abstract.Library;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Application.PaymentManagement.Commands.Create;
using Application.PaymentManagement.Commands.Save;
using Application.PaymentManagement.Commands.Verify;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;

namespace NET1061_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        public readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IMediator mediator,
                                 IConfiguration config,
                                 IOrderRepository orderRepository,
                                 IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _config = config;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("create-vnpay")]
        public async Task<IActionResult> CreateVnPayUrl([FromBody] CreateVnPayPaymentUrlDto command)
        {
            var request = new CreateVnPayPaymentUrlCommand(command);
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        // 2. Xác thực callback từ VNPAY (thường dùng ở /vnpay/return hoặc /vnpay/ipn)
        //[HttpGet("vnpay-return")]
        //public async Task<IActionResult> VerifyVnPayCallback()
        //{
        //    var queryParams = Request.Query
        //        .ToDictionary(q => q.Key, q => q.Value.ToString());

        //    var command = new VerifyVnPayPaymentCommand(queryParams);
        //    var result = await _mediator.Send(command);

        //    return StatusCode(result.StatusCode, result);
        //}


        // 3. Lưu kết quả thanh toán vào hệ thống (thường sau khi xác minh xong)
        //[HttpPost("save-payment")]
        //public async Task<IActionResult> SavePaymentResult([FromBody] SavePaymentResultCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return StatusCode(result.StatusCode, result);
        //}

        [HttpGet("ipn")]
        public async Task<IActionResult> Ipn()
        {
            var vnp = new VnPayLibrary();

            if (!vnp.ValidateSignature(Request.Query, _config["Vnpay:HashSecret"]))
                return Content("INVALID_SIGNATURE");

            if (!Guid.TryParse(Request.Query["vnp_TxnRef"], out var orderId))
                return Content("INVALID_ORDER_ID");

            var status = Request.Query["vnp_TransactionStatus"];
            var responseCode = Request.Query["vnp_ResponseCode"];

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return Content("ORDER_NOT_FOUND");

            if (status == "00" && responseCode == "00")
            {
                order.Status = Domain.Enum.OrderStatus.Completed;
                _orderRepository.Update(order);
                await _unitOfWork.SaveChangesAsync();
                return Content("SUCCESS");
            }

            return Content("FAIL");
        }

        [HttpGet("return-paypal")]
        public async Task<IActionResult> ReturnPaypal(string orderId, string token, string PayerID)
        {
            Console.WriteLine("🔁 PayPal RETURN URL triggered");
            Console.WriteLine($"orderId = {orderId}, token = {token}, PayerID = {PayerID}");
            return Redirect($"/order/{orderId}"); // hoặc xử lý logic xác nhận đơn hàng
        }

        [HttpGet("return")]
        public async Task<IActionResult> Return()
        {
            var vnpLib = new VnPayLibrary();
            var hashSecret = _config["VnPay:HashSecret"];

            bool isValid = vnpLib.ValidateSignature(Request.Query, hashSecret);

            if (!isValid)
            {
                Console.WriteLine("❌ Sai chữ ký từ VNPAY");
                return BadRequest("Sai chữ ký");
            }

            Console.WriteLine("✅ Chữ ký hợp lệ từ VNPAY");

            // ✅ Bạn có thể xử lý tiếp đơn hàng ở đây (cập nhật trạng thái)
            var orderId = Request.Query["vnp_TxnRef"].ToString();

            return Ok(new
            {
                Message = "Thanh toán thành công",
                OrderId = orderId
            });
        }

        //[HttpGet("VnPayReturn")]
        //public IActionResult VnPayReturn()
        //{
        //    var hashSecret = _config["VnPay:HashSecret"];
        //    var vnpLib = new VnPayLibrary();

        //    bool isValid = vnpLib.ValidateSignature(Request.Query, hashSecret);

        //    var vnpSecureHash = Request.Query["vnp_SecureHash"].ToString();

        //    // Tính lại chữ ký để so sánh trực tiếp
        //    var data = Request.Query
        //        .Where(kvp => kvp.Key.StartsWith("vnp_") && kvp.Key != "vnp_SecureHash" && kvp.Key != "vnp_SecureHashType")
        //        .OrderBy(kvp => kvp.Key)
        //        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

        //    string Encode(string key, string value)
        //    {
        //        return key == "vnp_OrderInfo"
        //            ? HttpUtility.UrlEncode(value, Encoding.UTF8)
        //            : Uri.EscapeDataString(value);
        //    }
        //    var receivedHash = Request.Query["vnp_SecureHash"].ToString();
        //    string signData = string.Join("&", data.Select(kvp => $"{kvp.Key}={Encode(kvp.Key, kvp.Value)}"));
        //    string localHash = VnPayLibrary.ComputeHash(hashSecret, signData);

        //    Console.WriteLine("✅ Received Hash = " + receivedHash);
        //    Console.WriteLine("🔍 SignData = " + signData);
        //    Console.WriteLine("✅ Local Hash = " + localHash);
        //    Console.WriteLine("🔁 Received Hash = " + vnpSecureHash);

        //    if (!string.Equals(localHash, vnpSecureHash, StringComparison.OrdinalIgnoreCase))
        //    {
        //        Console.WriteLine("❌ CHỮ KÝ KHÔNG TRÙNG KHỚP");
        //        return BadRequest("Sai chữ ký");
        //    }

        //    Console.WriteLine("✅ CHỮ KÝ TRÙNG KHỚP");

        //    // TODO: xử lý cập nhật đơn hàng
        //    var orderId = Request.Query["vnp_TxnRef"].ToString();
        //    return Ok(new
        //    {
        //        Message = "Thanh toán thành công",
        //        OrderId = orderId
        //    });
        //}

    }
}
