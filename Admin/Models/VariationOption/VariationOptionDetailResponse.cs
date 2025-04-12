namespace Admin.Models.VariationOption
{
    public class VariationOptionDetailResponse
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string VariationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
