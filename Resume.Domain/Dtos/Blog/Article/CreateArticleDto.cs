using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Resume.Domain.Dtos.Blog.Article
{
    public class CreateArticleDto
    {
        public long ArticleCategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? Description { get; set; }

        //public Entities.Blog.ArticleCategory ArticleCategory { get; set; }
    }

    public class CreateArticleResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static CreateArticleResult Success() => new CreateArticleResult(true);
        public static CreateArticleResult Failed(string message) => new CreateArticleResult(false, message);
    }
}
