using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Resume.Education
{
    public class FilterEducationDto
    {
        public long Id { get; set; }
        public required string UnivercityName { get; set; }
        public required string EducatioStartDate { get; set; }
        public required string EducationEndDate { get; set; }
        public required string Description { get; set; }
        public required string CreateDate { get; set; }
    }
}
