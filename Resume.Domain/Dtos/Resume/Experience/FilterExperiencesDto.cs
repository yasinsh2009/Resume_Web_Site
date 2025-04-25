namespace Resume.Domain.Dtos.Resume.Experience
{
    public class FilterExperiencesDto
    {
        public long Id { get; set; }
        public required string CompanyName { get; set; }
        public required string JobStartDate { get; set; }
        public required string JobEndDate { get; set; }
        public required string Description { get; set; }
        public required string CreateDate { get; set; }
    }
}
