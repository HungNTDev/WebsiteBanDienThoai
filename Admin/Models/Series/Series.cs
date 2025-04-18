using System.Text.Json.Serialization;

namespace Admin.Models.Series
{
    public class Series
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}