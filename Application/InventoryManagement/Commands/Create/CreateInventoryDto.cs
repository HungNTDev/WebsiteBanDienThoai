using System.Text.Json.Serialization;

namespace Application.InventoryManagement.Commands.Create
{
    public class CreateInventoryDto
    {
        public int StockQuantity { get; set; }
        public Guid ProductItemId { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
