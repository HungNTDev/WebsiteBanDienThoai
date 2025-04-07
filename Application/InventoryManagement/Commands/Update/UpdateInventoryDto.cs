using System.Text.Json.Serialization;

namespace Application.InventoryManagement.Commands.Update
{
    public class UpdateInventoryDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int StockQuantity { get; set; }
        public int? ReservedQuantity { get; set; }
        public Guid ProductItemId { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
