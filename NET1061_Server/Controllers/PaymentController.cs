using Application.Abstract.Library;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Application.Abstract.Services;
using Application.PaymentManagement.Commands.Create;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;


        public PaymentController(IMediator mediator,
                                 IConfiguration config,
                                 IOrderRepository orderRepository,
                                 IUnitOfWork unitOfWork,
                                 IEmailService emailService,
                                 UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _config = config;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _userManager = userManager;
        }
        [HttpPost("create-vnpay")]
        public async Task<IActionResult> CreateVnPayUrl([FromBody] CreateVnPayPaymentUrlDto command)
        {
            var request = new CreateVnPayPaymentUrlCommand(command);
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }


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
                order.Status = Domain.Enum.OrderStatus.Paid;
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
        [AllowAnonymous]
        public async Task<IActionResult> Return()
        {
            var user = _userManager.GetUserAsync(User).Result;
            try
            {
                var vnpLib = new VnPayLibrary();
                var hashSecret = _config["VnPay:HashSecret"];

                // ✅ Kiểm tra chữ ký
                bool isValid = vnpLib.ValidateSignature(Request.Query, hashSecret);
                if (!isValid)
                {
                    Console.WriteLine("❌ Chữ ký không hợp lệ.");
                    return Redirect("/order/failed");
                }

                // ✅ Lấy orderId từ vnp_TxnRef
                var orderId = Request.Query["vnp_TxnRef"].ToString();
                if (string.IsNullOrWhiteSpace(orderId))
                {
                    Console.WriteLine("❌ Không có OrderId (vnp_TxnRef)");
                    return Redirect("/order/failed");
                }

                // ✅ In ra để debug
                Console.WriteLine("✅ Thanh toán hợp lệ cho đơn hàng: " + orderId);
                var order = Request.Query["vnp_TxnRef"].ToString();

                var subject = $" Xác nhận đã thanh toán thành công đơn hàng: {orderId}";
                var body = $@"
                  Chào {user.UserName},<br/><br/>
                     Chúc mừng đã thanh toán thành công đơn hàng: {orderId}<br/><br/>
                     
                      Nếu có bất kỳ vấn đề nào về đơn hàng, vui lòng liên hệ với chúng tôi qua email hoặc số điện thoại bên dưới.<br/><br/>
                       Số Hotline: 18002063 (8h00 - 21h30)<br><br/>

                     Đội ngũ hỗ trợ cellphoneS.
                    
                ";

                await _emailService.SendAsync(user.Email, subject, body);
                // ✅ Điều hướng về trang chi tiết đơn hàng
                return Redirect($"https://localhost:7001/order/{orderId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Exception khi xử lý return từ VNPAY: " + ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
