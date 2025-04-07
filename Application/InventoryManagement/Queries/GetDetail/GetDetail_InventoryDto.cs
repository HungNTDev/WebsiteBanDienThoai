using System.Text.Json.Serialization;

namespace Application.InventoryManagement.Queries.GetDetail
{
    public class GetDetail_InventoryDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        [JsonIgnore]
        public Guid ProductItemId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
