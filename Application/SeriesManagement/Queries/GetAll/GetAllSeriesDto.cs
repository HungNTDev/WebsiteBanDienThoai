namespace Application.SeriesManagement.Queries.GetAll
{
    public class GetAllSeriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
