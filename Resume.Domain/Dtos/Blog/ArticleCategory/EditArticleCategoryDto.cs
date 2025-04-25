using Resume.Domain.Dtos.Project;

namespace Resume.Domain.Dtos.Blog.ArticleCategory
{
    public class EditArticleCategoryDto : CreateArticleCategoryDto
    {
        public long Id { get; set; }
    }
    public class EditArticleCategoryResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static EditArticleCategoryResult Success() => new EditArticleCategoryResult(true);
        public static EditArticleCategoryResult Failed(string message) => new EditArticleCategoryResult(false, message);
        public static EditArticleCategoryResult NotFound(string message) => new EditArticleCategoryResult(false, message);
    }
}
