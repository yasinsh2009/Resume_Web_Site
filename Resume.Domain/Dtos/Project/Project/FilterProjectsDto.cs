using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Project.Project
{
    public class FilterProjectsDto
    {
        public long Id { get; set; }
        public required string ProjectCategoryTitle { get; set; }
        public required string ProjectImage { get; set; }
        public required string ProjectTitle { get; set; }
        public required string? Description { get; set; }
        public required string CreateDate { get; set; }
    }
}
