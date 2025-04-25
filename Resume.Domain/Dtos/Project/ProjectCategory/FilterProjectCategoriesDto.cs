using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Project.ProjectCategory
{
    public class FilterProjectCategoriesDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string CreateDate { get; set; }
    }
}
