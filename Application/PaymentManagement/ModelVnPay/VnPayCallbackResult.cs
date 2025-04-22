using Domain.Enum;

namespace Application.PaymentManagement.ModelVnPay
{
    public class VnPayCallbackResult
    {
        public bool IsSuccess { get; set; }              // Chữ ký có hợp lệ không
        public Guid OrderId { get; set; }                // Mã đơn hàng
        public string? TransactionId { get; set; }       // Mã giao dịch VNPAY
        public decimal Amount { get; set; }              // Số tiền thanh toán
        public PaymentStatus Status { get; set; }        // Thành công / Thất bại
        public Guid UserId { get; set; }                 // (tuỳ chọn) Người dùng thực hiện thanh toán
        public Guid PaymentTypeId { get; set; }          // (tuỳ chọn) Loại thanh toán (VNPAY)
    }
}
