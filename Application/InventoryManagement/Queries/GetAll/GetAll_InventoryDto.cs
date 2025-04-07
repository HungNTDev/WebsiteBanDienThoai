using System.Text.Json.Serialization;

namespace Application.InventoryManagement.Queries.GetAll
{
    public class GetAll_InventoryDto
    {
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        [JsonIgnore]
        public Guid ProductItemId { get; set; }
        public string ProductName { get; set; }
    }
}
