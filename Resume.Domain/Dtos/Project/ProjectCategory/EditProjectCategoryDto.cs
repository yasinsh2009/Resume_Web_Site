using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Project.ProjectCategory
{
    public class EditProjectCategoryDto : CreateProjectCategoryDto
    {
        public long Id { get; set; }
    }

    public class EditProjectCategoryResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; set; } = isSuccess;
        public string? Message { get; set; } = message;

        public static EditProjectCategoryResult Success() => new EditProjectCategoryResult(true);
        public static EditProjectCategoryResult Failed(string message) => new EditProjectCategoryResult(false, message);
    }
}
