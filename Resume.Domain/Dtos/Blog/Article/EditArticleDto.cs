using Resume.Domain.Dtos.Project;

namespace Resume.Domain.Dtos.Blog.Article
{
    public class EditArticleDto : CreateArticleDto
    {
        public long Id { get; set; }
    }
    public class EditArticleResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static EditArticleResult Success() => new EditArticleResult(true);
        public static EditArticleResult Failed(string message) => new EditArticleResult(false, message);
        public static EditArticleResult NotFound(string message) => new EditArticleResult(false, message);
    }
}
